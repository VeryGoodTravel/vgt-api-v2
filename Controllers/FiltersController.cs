using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("GetFilters")]
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
                Filters filters = new Filters();
                // TODO: Implement filters logic
                return Envelope<Filters>.Ok(filters);
            } catch (Exception e)
            {
                return Envelope<Filters>.Error(e.Message);
            }
        }
    }
}
