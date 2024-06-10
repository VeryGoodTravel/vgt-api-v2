using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Responses;

public class StatsResponse
{
    [JsonProperty("directions")]
    public Direction[] Directions { get; set; }
    
    [JsonProperty("accommodations")]
    public Accommodation[] Accommodations { get; set; }
}