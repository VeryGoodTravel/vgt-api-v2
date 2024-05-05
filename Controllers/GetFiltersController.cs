using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Common;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetFiltersController : ControllerBase
    {
        private readonly ILogger<GetFiltersController> _logger;

        public GetFiltersController(ILogger<GetFiltersController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetFilters")]
        public GetFilters Get()
        {
            return new GetFilters
            {
                Participants = new Participant[]
                {
                    new Participant
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Adult",
                        Max = 5,
                        Min = 1
                    },
                    new Participant
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Child",
                        Max = 3,
                        Min = 0
                    }
                },
                Destinations = new TravelLocation[]
                {
                    new TravelLocation
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Egypt",
                        Locations = new TravelLocation[]
                        {
                            new TravelLocation
                            {
                                Id = Guid.NewGuid().ToString(),
                                Label = "Hurghada"
                            },
                            new TravelLocation
                            {
                                Id = Guid.NewGuid().ToString(),
                                Label = "Sharm El Sheikh"
                            }
                        }

                    },
                    new TravelLocation
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Austria"
                    }
                },
                Origins = new TravelLocation[]
                {
                    new TravelLocation
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Warsaw"
                    },
                    new TravelLocation
                    {
                        Id = Guid.NewGuid().ToString(),
                        Label = "Berlin"
                    }
                }
            };
            
        }
    }
}
