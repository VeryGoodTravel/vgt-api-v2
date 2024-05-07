using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetOfferDetails")]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;

        public OfferController(ILogger<OfferController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Envelope<TravelOffer> GetOffer([FromQuery] OfferRequest offerRequest)
        {
            try
            {
                TravelOffer travelOffer = new TravelOffer();
                // TODO: Implement offer logic
                return Envelope<TravelOffer>.Ok(travelOffer);
            } catch (Exception e)
            {
                return Envelope<TravelOffer>.Error(e.Message);
            }
            
        }
    }
}
