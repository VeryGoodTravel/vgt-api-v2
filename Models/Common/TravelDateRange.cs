namespace vgt_api.Models.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes travel date range.
    /// </summary>
    public partial class TravelDateRange
    {
        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }
    }
}
