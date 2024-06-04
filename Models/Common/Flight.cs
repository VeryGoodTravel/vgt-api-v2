using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Flight
{
    // only used when getting a single offer
    [JsonProperty("available")]
    public bool Available { get; set; }
    
    [JsonProperty("flightId")]
    public string FlightId { get; set; }
    
    [JsonProperty("departureAirportCode")]
    public string DepartureAirportCode { get; set; }
    
    [JsonProperty("departureAirportName")]
    public string DepartureAirportName { get; set; }
    
    [JsonProperty("arrivalAirportCode")]
    public string ArrivalAirportCode { get; set; }
    
    [JsonProperty("arrivalAirportName")]
    public string ArrivalAirportName { get; set; }
    
    [JsonProperty("departureDate")]
    public string DepartureDate { get; set; }
    
    [JsonProperty("price")]
    public decimal Price { get; set; }
}