using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Requests.Hotels;

public class HotelRequest
{
    [JsonProperty("hotel_id")]
    public string HotelId { get; set; }
    
    [JsonProperty("room_id")]
    public string RoomId { get; set; }
    
    [JsonProperty("participants")]
    public Dictionary<int, int> Participants { get; set; }
    
    [JsonProperty("dates")]
    public TravelDateRange Dates { get; set; }
}