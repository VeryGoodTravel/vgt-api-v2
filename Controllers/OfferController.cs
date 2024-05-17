using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetOfferDetails")]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;
        private readonly OffersService _offersService;

        public OfferController(ILogger<OfferController> logger, OffersService offersService)
        {
            _logger = logger;
            _offersService = offersService;
        }

        [HttpGet]
        public async Task<Envelope<TravelOffer>> GetOffer([FromQuery] OfferRequest offerRequest)
        {
            try
            {
                var travelOffer = await _offersService.GetOffer(offerRequest.OfferId);
                return Envelope<TravelOffer>.Ok(travelOffer);
            } catch (Exception e)
            {
                return Envelope<TravelOffer>.Error(e.Message);
            }
            
        }
    }
}
