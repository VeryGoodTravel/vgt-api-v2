using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;
using vgt_api.Models.Rabbit;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/PurchaseOffer")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly JwtService _jwtService;

        public PurchaseController(ILogger<PurchaseController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
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
