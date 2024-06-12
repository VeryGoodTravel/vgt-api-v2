using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Responses;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/GetPopularOffers")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly StatsService _statsService;
        private readonly HotelService _hotelService;
        private readonly FlightService _flightService;

        public StatsController(StatsService statsService, HotelService hotelService, FlightService flightService, ILogger<StatsController> logger)
        {
            _statsService = statsService;
            _hotelService = hotelService;
            _flightService = flightService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Envelope<StatsResponse>> GetPopularOffers()
        {
            _logger.LogInformation("Received GetPopularOffers request");
            try
            {
                var stats = await _statsService.GetStats();
                var destinations = (await _hotelService.GetLocations()).Locations;
                var origins = (await _flightService.GetDepartureAirports()).Airports;
                _logger.LogInformation("Received stats from vgt-stats: {stats}", stats);

                var allDestinations = destinations.SelectMany(d => d.Locations).ToList();
                
                var directions = stats.Directions.Select(d => new Direction
                {
                    Origin = new TravelLocation
                    {
                        Id = origins.Find(o => o.AirportName.Equals(d.Origin.Replace("_", " ")))?.AirportCode,
                        Label = d.Origin.Replace("_", " ")
                    },
                    Destination = new TravelLocation
                    {
                        Id = allDestinations.Find(ad => ad.Label.Equals(d.Destination.Replace("_", " ")))?.Id,
                        Label = d.Destination.Replace("_", " ")
                    }
                }).ToArray();

                var accommodations = stats.Accommodations.Select(a => new Accommodation
                {
                    Destination = new TravelLocation
                    {
                        Id = allDestinations.Find(ad => ad.Label.Equals(a.Destination.Replace("_", " ")))?.Id,
                        Label = a.Destination.Replace("_", " ")
                    },
                    Maintenance = a.Maintenance.Replace("_", " "),
                    Name = a.Name.Replace("_", " "),
                    Rating = 0.0,
                    Room = a.Room.Replace("_", " "),
                    Transportation = a.Transportation.Replace("_", " ")
                }).ToArray();

                var response = new StatsResponse
                {
                    Directions = directions,
                    Accommodations = accommodations
                };
                
                _logger.LogInformation("Returning GetPopularOffers successfully");
                return Envelope<StatsResponse>.Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError("Thrown error in GetPopularOffers request handling: {error}", e.Message);
                _logger.LogError("Stacktrace: {error}", e.StackTrace);
                return Envelope<StatsResponse>.Error(e.Message);
            }
        }
    }
}

