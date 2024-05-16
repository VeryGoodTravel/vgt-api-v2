using Newtonsoft.Json;

namespace vgt_api.Models.Requests.Flights;

public class FlightRequest
{
    [JsonProperty("flight_id")]
    public string FlightId { get; set; }
    
    [JsonProperty("number_of_passengers")]
    public int NumberOfPassengers { get; set; }
    
    public FlightRequest(string flightId, int numberOfPassengers)
    {
        FlightId = flightId;
        NumberOfPassengers = numberOfPassengers;
    }
}