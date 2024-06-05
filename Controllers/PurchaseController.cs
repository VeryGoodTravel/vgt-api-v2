using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;
using vgt_api.Models.Rabbit;
using vgt_api.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/PurchaseOffer")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly JwtService _jwtService;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _backendToSaga;
        private readonly IModel _sagaToBackend;
        private EventingBasicConsumer? _consumer;
        
        public PurchaseController(ILogger<PurchaseController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
            
            while (_connection is not { IsOpen: true })
            {
            
                try
                {
                    _factory = new ConnectionFactory
                    {
                        HostName = "vgt-broker",
                        Port = 5672,
                        UserName = "verygoodtravel",
                        Password = "verygoodtravel",
                        VirtualHost = "vgt-vhost",
                        RequestedHeartbeat = TimeSpan.FromSeconds(60),
                        RequestedConnectionTimeout = TimeSpan.FromSeconds(6000)
                    };
                    _connection = _factory.CreateConnection();
                }
                catch (BrokerUnreachableException e)
                {
                    _logger.LogInformation("Rabbit connection failed");
                }
                Task.Delay(100).Wait();
            }
            
            _backendToSaga = _connection.CreateModel();
            _backendToSaga.QueueDeclare("backend-to-saga-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: new Dictionary<string, object>());
            
            _sagaToBackend = _connection.CreateModel();
            _sagaToBackend.ExchangeDeclare("saga-to-backends", 
                ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: new Dictionary<string, object>());
            
        }

        [HttpPost]
        public async Task<Envelope<PurchaseResponse>> Purchase([FromBody] PurchaseRequest request)
        {
            try
            {
                if (!_jwtService.VerifyJwtToken(request.Token))
                    return Envelope<PurchaseResponse>.Error("Unauthorized");
            } catch (Exception e)
            {
                return Envelope<PurchaseResponse>.Error($"Unauthorized: {e.Message}");
            }

            PurchaseResponse purchaseResponse = await PurchaseOffer(request.OfferId);
            return Envelope<PurchaseResponse>.Ok(purchaseResponse);
        }

        private async Task<PurchaseResponse> PurchaseOffer(string offerId)
        {
            try
            {
                var filters = IdFilters.FromId(offerId);
                
                var transaction = new Transaction() {
                    TransactionId = Guid.NewGuid(),
                    OfferId = offerId,
                    BookFrom = DateTime.ParseExact(filters.Dates.Start, "dd-MM-yyyy", null),
                    BookTo = DateTime.ParseExact(filters.Dates.End, "dd-MM-yyyy", null),
                    TripFrom = filters.City,
                    HotelName = filters.HotelName,
                    RoomType = filters.RoomName,
                    AdultCount = filters.Adults,
                    OldChildren = filters.Children18,
                    MidChildren = filters.Children10,
                    LesserChildren = filters.Children3,
                };
                
                // TODO: Implement purchase logic
                var bodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transaction));
                _backendToSaga.BasicPublish(string.Empty, "backend-to-saga-queue", null, bodyBytes);
                // listen for replay with same transactionId

                var sagaResponse = new SagaReply()
                {
                    Answer = SagaAnswer.Success
                };
                if (sagaResponse.Answer == SagaAnswer.Success)
                {
                    return new PurchaseResponse
                    {
                        Success = true,
                        Message = "Offer purchased successfully"
                    };
                }
                
                {
                    return new PurchaseResponse
                    {
                        Success = false,
                        Message = "Offer purchase failed"
                    };
                }
            } catch (Exception e)
            {
                return new PurchaseResponse
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }
    }
}
