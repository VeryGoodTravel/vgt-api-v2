using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/PurchaseOffer")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(ILogger<PurchaseController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Envelope<PurchaseResponse> Purchase([FromBody] PurchaseRequest request)
        {
            try
            {
                if (!JwtHandler.VerifyJwtToken(request.Token))
                    return Envelope<PurchaseResponse>.Error("Unauthorized");
            } catch (Exception e)
            {
                return Envelope<PurchaseResponse>.Error($"Unauthorized: {e.Message}");
            }

            PurchaseResponse purchaseResponse = PurchaseOffer();
            return Envelope<PurchaseResponse>.Ok(purchaseResponse);
        }

        private PurchaseResponse PurchaseOffer()
        {
            try
            {
                // TODO: Implement purchase logic
                return new PurchaseResponse
                {
                    Success = true
                };
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
