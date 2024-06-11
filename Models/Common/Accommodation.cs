using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Accommodation
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

    public static Accommodation GetExample()
    {
        return new Accommodation
        {
            Destination = "New York",
            Name = "Warwick New York",
            Room = "Double",
            Transportation = "Plane",
            Maintenance = "All inclusive"
        };
    }
}