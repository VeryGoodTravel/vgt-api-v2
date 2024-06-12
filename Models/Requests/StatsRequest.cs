using Newtonsoft.Json;

namespace vgt_api.Models.Requests;

public class StatsRequest
{
    [JsonProperty("offer_id")]
    public string OfferId { get; set; }
}