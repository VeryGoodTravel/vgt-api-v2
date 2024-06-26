using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Accommodation
{
    [JsonProperty("destination")]
    public TravelLocation Destination { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("room")]
    public string Room { get; set; }
    
    [JsonProperty("transportation")]
    public string Transportation { get; set; }
    
    [JsonProperty("maintenance")]
    public string Maintenance { get; set; }
    
    [JsonProperty("rating")]
    public double Rating { get; set; }

    public static Accommodation GetExample()
    {
        return new Accommodation
        {
            Destination = new TravelLocation
            {
                Id = "20",
                Label = "New York",
            },
            Name = "Warwick New York",
            Room = "Double",
            Transportation = "Samolot",
            Maintenance = "All Inclusive",
            Rating = 4.5
        };
    }
}