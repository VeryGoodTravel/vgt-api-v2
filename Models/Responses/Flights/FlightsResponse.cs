using Newtonsoft.Json;

namespace vgt_api.Models.Responses.Flights;

public class FlightsResponse
{
    [JsonProperty("flights")]
    public List<FlightResponse> Flights { get; set; }
}