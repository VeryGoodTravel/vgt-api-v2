using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Responses;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetFilters")]
    public class FiltersController : ControllerBase
    {
        private readonly ILogger<FiltersController> _logger;
        private readonly FlightService _flightService;
        private readonly HotelService _hotelService;

        public FiltersController(ILogger<FiltersController> logger, 
            FlightService flightService,
            HotelService hotelService)
        {
            _logger = logger;
            _flightService = flightService;
            _hotelService = hotelService;
        }

        [HttpGet]
        public async Task<Envelope<Filters>> GetFilters()
        {
            try
            {
                Filters filters = new Filters
                {
                    Origins = await GetOrigins(),
                    Destinations = await GetDestinations(),
                    Dates = GetDates(),
                    Participants = GetParticipants()
                };
                return Envelope<Filters>.Ok(filters);
            } catch (Exception e)
            {
                return Envelope<Filters>.Error(e.Message);
            }
        }

        private Participants GetParticipants()
        {
            return Participants.GetExample();
        }

        private TravelDateRange GetDates()
        {
            return TravelDateRange.GetExample();
        }

        private async Task<TravelLocation[]> GetDestinations()
        {
            var response = await _hotelService.GetLocations();
            
            return response.Locations.ToArray();
        }

        private async Task<TravelLocation[]> GetOrigins()
        {
            var response = await _flightService.GetDepartureAirports();
            
            var airportsInPoland = new TravelLocation()
            {
                Id = "Polska", Label = "Polska",
                Locations = response.Airports.Select(airport => new TravelLocation()
                {
                    Id = airport.AirportCode,
                    Label = airport.AirportName
                }).ToArray()
            };
            
            return new[] { airportsInPoland };
        }
    }
    
    
}
