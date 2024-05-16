using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Responses.Hotels;

public class HotelsResponse
{
    [JsonProperty("hotels")]
    public List<Hotel> Hotels { get; set; }
}