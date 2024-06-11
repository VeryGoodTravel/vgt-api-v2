using System.Collections.Concurrent;
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
        private ConcurrentDictionary<Guid, SagaReply> _sagaResponses;
        
        public PurchaseController(ILogger<PurchaseController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
            _sagaResponses = new ConcurrentDictionary<Guid, SagaReply>();
            
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
            
            _logger.LogInformation("During RABBITMQ initialization -----------");
            logger.LogInformation("During RABBITMQ backend -----------");
            _backendToSaga = _connection.CreateModel();
            _backendToSaga.QueueDeclare("backend-to-saga-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: new Dictionary<string, object>());
            
            logger.LogInformation("During RABBITMQ exchange -----------");
            _sagaToBackend = _connection.CreateModel();
            _sagaToBackend.ExchangeDeclare("saga-to-backends", 
                ExchangeType.Fanout,
                durable: true,
                autoDelete: false,
                arguments: new Dictionary<string, object>());
            
            logger.LogInformation("During RABBITMQ tempqueue -----------");
            var queueName = _sagaToBackend.QueueDeclare().QueueName;
            _sagaToBackend.QueueBind(queue: queueName,
                exchange: "saga-to-backends",
                routingKey: string.Empty);
            
            logger.LogInformation("During RABBITMQ consumer -----------");
            var consumer = new EventingBasicConsumer(_sagaToBackend);
            consumer.Received += (model, ea) =>
            {
                _logger.LogInformation("Received response from the queue");
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation(message);
                var reply = JsonConvert.DeserializeObject<SagaReply>(message);
                if (!_sagaResponses.TryAdd(reply.TransactionId, reply))
                    _logger.LogError("Failed to add saga response to concurrent dictionary");
                _logger.LogInformation("Added saga respone to SagaResponses");
            };
            _sagaToBackend.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
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
                _logger.LogInformation("Received PurchaseOffer request {offerId}", offerId);
                
                var filters = IdFilters.FromId(offerId);
                _logger.LogInformation("PurchaseOffer filters: {filters}", JsonConvert.SerializeObject(filters));
                
                var transactionId = Guid.NewGuid();
                var transaction = new Transaction() {
                    TransactionId = transactionId,
                    OfferId = offerId,
                    BookFrom = DateTime.ParseExact(filters.Dates.Start, "dd-MM-yyyy", null),
                    BookTo = DateTime.ParseExact(filters.Dates.End, "dd-MM-yyyy", null),
                    TripFrom = filters.DepartureCity,
                    TripTo = filters.ArrivalCity,
                    HotelName = filters.HotelName,
                    RoomType = filters.RoomName,
                    AdultCount = filters.Adults,
                    OldChildren = filters.Children18,
                    MidChildren = filters.Children10,
                    LesserChildren = filters.Children3,
                };
                _logger.LogInformation("PurchaseOffer transaction: {transaction}",
                    JsonConvert.SerializeObject(transaction));
                
                // TODO: Implement purchase logic
                var bodyBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transaction));
                _backendToSaga.BasicPublish(string.Empty, "backend-to-saga-queue", null, bodyBytes);
                
                // listen for reply with same transactionId
                var timeout = 60;
                SagaReply sagaResponse;
                while (timeout > 0)
                {
                    if (_sagaResponses.TryRemove(transactionId, out sagaResponse))
                    {
                        if (sagaResponse.Answer == SagaAnswer.Success)
                        {
                            _logger.LogInformation("PurchaseOffer saga returned and succeeded");
                            return new PurchaseResponse
                            {
                                Success = true,
                                Message = "Offer purchased successfully"
                            };
                        }
                        
                        {
                            _logger.LogInformation("PurchaseOffer saga returned but purchase failed");
                            return new PurchaseResponse
                            {
                                Success = false,
                                Message = "Offer purchase failed"
                            };
                        }
                    }
                    await Task.Delay(1000);
                    timeout--;
                }
                
                _logger.LogInformation("PurchaseOffer timeouted");
                return new PurchaseResponse
                {
                    Success = false,
                    Message = "Offer purchase failed"
                };
            }
            catch (Exception e)
            {
                _logger.LogError("Thrown error in PurchaseOffer request handling: {error}", e.Message);
                _logger.LogError("Stacktrace: {error}", e.StackTrace);
                return new PurchaseResponse
                {
                    Success = false,
                    Message = e.Message
                };
            }
        }
    }
}
