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

        [HttpGet]
        public async Task<Envelope<SearchResults>> Search([FromQuery] SearchFilters request)
        {
            try
            {
                var results  = 
                    await _offersService.GetOffers((request.Page-1)*OffersPerPage, OffersPerPage, request);

                var offers = results.Item1;
                var count = results.Item2;
                if (count == 0)
                {
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
                
                return Envelope<SearchResults>.Ok(searchResults);
            }
            catch (Exception e)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(e.StackTrace));
                return Envelope<SearchResults>.Error(e.Message);
            }
        }

        private TravelOffer[] GetOffers(SearchFilters filters)
        {
            // TODO: Implement search logic
            
            int children18;
            int children10;
            int children3;
            filters.Participants.TryGetValue(3, out children18);
            filters.Participants.TryGetValue(2, out children10);
            filters.Participants.TryGetValue(1, out children3);
            
            List<TravelOffer> offers = new List<TravelOffer>();
            
            for (int i = 0; i < 11; i++)
            {
                IdFilters idFilters = new IdFilters
                {
                    HotelId = $"hotel_{i}",
                    HotelName = $"hotel name {i}",
                    RoomId = $"room_{i}",
                    RoomName = $"room name {i}",
                    City = $"location {i}",
                    FlightToId = $"flight_to_{i}",
                    FlightFromId = $"flight_from_{i}",
                    Adults = filters.Participants[4],
                    Children18 = children18,    
                    Children10 = children10,
                    Children3 = children3,
                    Dates = filters.Dates
                };

                var exampleOffer = TravelOffer.GetExample();
                exampleOffer.Id = idFilters.ToString();
                
                offers.Add(exampleOffer);
            }

            return offers.ToArray();
        }
    }
}
