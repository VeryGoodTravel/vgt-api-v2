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

        public StatsController(StatsService statsService, ILogger<StatsController> logger)
        {
            _statsService = statsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Envelope<StatsResponse>> GetPopularOffers()
        {
            _logger.LogInformation("Received GetPopularOffers request");
            try
            {
                var stats = await _statsService.GetStats();
                _logger.LogInformation("Received stats from vgt-stats: {stats}", stats);

                var directions = stats.Directions.Select(d => new Direction
                {
                    Origin = new TravelLocation
                    {
                        Id = "0", // uzupelnic id z getfilters
                        Label = d.Origin.Replace("_", " ")
                    },
                    Destination = new TravelLocation
                    {
                        Id = "0", // uzupelnic id z getfilters
                        Label = d.Destination.Replace("_", " ")
                    }
                }).ToArray();

                var accommodations = stats.Accommodations.Select(a => new Accommodation
                {
                    Destination = new TravelLocation
                    {
                        Id = "0", // uzupelnic id z getfilters
                        Label = a.Destination.Replace("_", " ")
                    },
                    Maintenance = a.Maintenance.Replace("_", " "),
                    Name = a.Name.Replace("_", " "),
                    Rating = 5.0, // ogarnac ten rating z hotelservice
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

