using Newtonsoft.Json;

namespace vgt_api.Models.Common
{
    /// <summary>
    /// Meta-data regarding pagination.
    /// </summary>
    public class Pages
    {
        /// <summary>
        /// Page number
        /// </summary>
        [JsonProperty("page")]
        public long Page { get; set; }

        /// <summary>
        /// Total amount of offers on all pages (not an amount of available pages)
        /// </summary>
        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
