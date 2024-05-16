using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Requests.Hotels;

public class HotelsRequest
{
    [JsonProperty("dates")]
    public TravelDateRange Dates { get; set; }
    
    // If null, all cities are considered
    [JsonProperty("cities")]
    public List<string>? Cities { get; set; }
    
    [JsonProperty("participants")]
    public Dictionary<int, int> Participants { get; set; }
}