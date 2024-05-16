using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Room
{
    // only used when getting a single offer
    [JsonProperty("available")]
    public bool Available { get; set; }
    
    [JsonProperty("room_id")]
    public string RoomId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("price_per_person")]
    public double Price { get; set; }
}