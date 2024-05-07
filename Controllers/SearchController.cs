using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetOfferPage")]
    public class SearchController : ControllerBase
    {
        private const int OffersPerPage = 5;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<Envelope<SearchResults>> Search([FromQuery] SearchFilters request)
        {
            try
            {
                TravelOffer[] offers = await GetOffers(request);
                
                Pages pages = new Pages
                {
                    Page = request.Page,
                    Total = (int)Math.Ceiling((decimal)offers.Length / OffersPerPage)
                };
                
                int toSkip = (pages.Page - 1) * OffersPerPage;
                SearchResults searchResults = new SearchResults
                {
                    Pages = pages,
                    Offers = offers.Skip(toSkip).Take(OffersPerPage).ToArray()
                };
                
                return Envelope<SearchResults>.Ok(searchResults);
            }
            catch (Exception e)
            {
                return Envelope<SearchResults>.Error(e.Message);
            }
        }

        private async Task<TravelOffer[]> GetOffers(SearchFilters filters)
        {
            // TODO: Implement search logic
            
            List<TravelOffer> offers = new List<TravelOffer>();
            
            for (int i = 0; i < 11; i++)
            {
                offers.Add(TravelOffer.GetExample());
            }

            return offers.ToArray();
        }
    }
}
