using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetOfferPage")]
    public class SearchController : ControllerBase
    {
        private const int OffersPerPage = 5;
        private readonly ILogger<SearchController> _logger;
        private readonly OffersService _offersService;

        public SearchController(ILogger<SearchController> logger, OffersService offersService)
        {
            _logger = logger;
            _offersService = offersService;
        }

        [HttpPost]
        public async Task<Envelope<SearchResults>> Search([FromBody] SearchFilters request)
        {
            _logger.LogInformation("Received GetOfferPage request: {request}",
                JsonConvert.SerializeObject(request));
            try
            {
                var results  = 
                    await _offersService.GetOffers((request.Page-1)*OffersPerPage, OffersPerPage, request);

                var offers = results.Item1;
                var count = results.Item2;
                _logger.LogInformation("Received offers from hotel and flight services - {count} offers", count);
                if (count == 0)
                {
                    _logger.LogInformation("Returning no offers, because there were none");
                    return Envelope<SearchResults>.Error("No offers found");
                }
                
                Pages pages = new Pages
                {
                    Page = request.Page,
                    Total = (int)Math.Ceiling((decimal)count / OffersPerPage)
                };
                
                SearchResults searchResults = new SearchResults
                {
                    Pages = pages,
                    Offers = offers.ToArray()
                };
                
                _logger.LogInformation("Returning GetOfferPage successfully");
                return Envelope<SearchResults>.Ok(searchResults);
            }
            catch (Exception e)
            {
                _logger.LogError("Thrown error in GetOfferPage request handling: {error}", e.Message);
                _logger.LogError("Stacktrace: {error}", e.StackTrace);
                return Envelope<SearchResults>.Error(e.Message);
            }
        }
    }
}
