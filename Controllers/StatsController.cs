using Microsoft.AspNetCore.Mvc;
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
                _logger.LogInformation("Returning GetPopularOffers successfully");
                return Envelope<StatsResponse>.Ok(stats);
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

