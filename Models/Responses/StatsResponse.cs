using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Responses;

public class StatsResponse
{
    [JsonProperty("popular_directions")]
    public PopularDirection[] PopularDirections { get; set; }
    
    [JsonProperty("popular_accommodations")]
    public PopularAccommodation[] PopularAccommodations { get; set; }
}