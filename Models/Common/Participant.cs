using Newtonsoft.Json;

namespace vgt_api.Models.Common
{
    public class Participant
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("min")]
        public long Min { get; set; }
    }
}