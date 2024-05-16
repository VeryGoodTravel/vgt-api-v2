using Newtonsoft.Json;
using vgt_api.Models.Responses.Hotels;

namespace vgt_api.Models.Common;

public class Hotel
{
    [JsonProperty("hotel_id")]
    public string HotelId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("city")]
    public string City { get; set; }
    
    [JsonProperty("country")]
    public string Country { get; set; }
    
    [JsonProperty("airport_code")]
    public string AirportCode { get; set; }
    
    [JsonProperty("rooms")]
    public List<Room> Rooms { get; set; }
}