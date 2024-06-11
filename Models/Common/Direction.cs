using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Direction
{
    [JsonProperty("origin")]
    public TravelLocation Origin { get; set; }
    
    [JsonProperty("destination")]
    public TravelLocation Destination { get; set; }

    public static Direction GetExample()
    {
        return new Direction
        {
            Origin = new TravelLocation
            {
                Id = "21",
                Label = "Warsaw",
            },
            Destination = new TravelLocation
            {
                Id = "20",
                Label = "New York",
            }
        };
    }
}