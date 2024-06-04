using Newtonsoft.Json;

namespace vgt_api.Models.Common;

public class Room
{
    [JsonProperty("roomId")]
    public string RoomId { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("price")]
    public decimal Price { get; set; }
}