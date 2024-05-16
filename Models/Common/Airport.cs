using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Airport
{
    [JsonProperty("airport_code")]
    public string AirportCode { get; set; }
    
    [JsonProperty("airport_name")]
    public string AirportName { get; set; }
}