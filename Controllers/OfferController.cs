using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
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
            _logger.LogInformation("Received GetOfferDetails request: {request}",
                JsonConvert.SerializeObject(offerRequest));
            try
            {
                var travelOffer = await _offersService.GetOffer(offerRequest.OfferId);
                _logger.LogInformation("Returning GetOfferDetails response: {response}",
                    JsonConvert.SerializeObject(travelOffer));
                return Envelope<TravelOffer>.Ok(travelOffer);
            }
            catch (Exception e)
            {
                _logger.LogError("Thrown error in GetOfferDetails request handling: {error}", e.Message);
                _logger.LogError("Stacktrace: {error}", e.StackTrace);
                return Envelope<TravelOffer>.Error(e.Message);
            }
            
        }
    }
}
