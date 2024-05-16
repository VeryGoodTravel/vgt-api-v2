using Newtonsoft.Json;

namespace vgt_api.Models.Requests.Flights;

public class FlightsRequest
{
    [JsonProperty("departure_airport_codes")]
    public List<string>? DepartureAirportCodes { get; set; }
    
    [JsonProperty("arrival_airport_codes")]
    public List<string>? ArrivalAirportCodes { get; set; }
    
    [JsonProperty("departure_date")]
    public string DepartureDate { get; set; }
    
    [JsonProperty("number_of_passengers")]
    public int NumberOfPassengers { get; set; }
}