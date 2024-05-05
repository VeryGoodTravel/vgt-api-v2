namespace vgt_api.Models.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes travel location, both origins and destinations.
    /// </summary>
    public partial class TravelLocation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("locations", NullValueHandling = NullValueHandling.Ignore)]
        public TravelLocation[] Locations { get; set; }
    }
}
