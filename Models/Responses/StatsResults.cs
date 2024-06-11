using Newtonsoft.Json;

namespace vgt_api.Models.Responses;

public class StatsDirection
{
    [JsonProperty("origin")]
    public string Origin { get; set; }
    
    [JsonProperty("destination")]
    public string Destination { get; set; }
}

public class StatsAccommodation
{
    [JsonProperty("destination")]
    public string Destination { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("room")]
    public string Room { get; set; }
    
    [JsonProperty("transportation")]
    public string Transportation { get; set; }
    
    [JsonProperty("maintenance")]
    public string Maintenance { get; set; }
}

public class StatsResults
{
    [JsonProperty("directions")]
    public StatsDirection[] Directions { get; set; }
    
    [JsonProperty("accommodations")]
    public StatsAccommodation[] Accommodations { get; set; }
}