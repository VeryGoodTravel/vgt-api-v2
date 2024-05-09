using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetFilters")]
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;

        public FiltersController(ILogger<FiltersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Envelope<Filters> GetFilters()
        {
            try
            {
                Filters filters = new Filters
                {
                    Origins = GetOrigins(),
                    Destinations = GetDestinations(),
                    Dates = GetDates(),
                    Participants = GetParticipants()
                };
                // TODO: Implement filters logic
                return Envelope<Filters>.Ok(filters);
            } catch (Exception e)
            {
                return Envelope<Filters>.Error(e.Message);
            }
        }

        private Participants GetParticipants()
        {
            // TODO: Implement participants logic
            return Participants.GetExample();
        }

        private TravelDateRange GetDates()
        {
            // TODO: Implement dates logic
            return TravelDateRange.GetExample();
        }

        private TravelLocation[] GetDestinations()
        {
            // TODO: Implement destinations logic
            return new[] { TravelLocation.GetExample() };
        }

        private TravelLocation[] GetOrigins()
        {
            // TODO: Implement origins logic
            return new[] { TravelLocation.GetExample() };
        }
    }
    
    
}
