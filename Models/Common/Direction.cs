using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Direction
{
    [JsonProperty("origin")]
    public string Origin { get; set; }
    
    [JsonProperty("destination")]
    public string Destination { get; set; }

    public static Direction GetExample()
    {
        return new Direction
        {
            Origin = "Warsaw",
            Destination = "New York"
        };
    }
}