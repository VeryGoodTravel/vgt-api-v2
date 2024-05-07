namespace vgt_api.Models.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes travel date range.
    /// </summary>
    public class TravelDateRange : Dictionary<string, string>
    {
        [JsonProperty("end")]
        public string End
        {
            get => this["end"];
            set => this["end"] = value;
        }

        [JsonProperty("start")]
        public string Start
        {
            get => this["start"];
            set => this["start"] = value;
        }
    }
}
