using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("GetOfferPage")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Envelope<SearchResults> Search([FromQuery] SearchFilters request)
        {
            try
            {
                SearchResults searchResults = new SearchResults();
                // TODO: Implement search logic
                return Envelope<SearchResults>.Ok(searchResults);
            } catch (Exception e)
            {
                return Envelope<SearchResults>.Error(e.Message);
            }
        }
    }
}
