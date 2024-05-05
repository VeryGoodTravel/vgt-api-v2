namespace vgt_api.Models.Responses
{
    using Newtonsoft.Json;
    using vgt_api.Models.Common;

    /// <summary>
    /// Contains page of offers, including meta-data about all available pages.
    /// </summary>
    public partial class GetOfferPage
    {
        /// <summary>
        /// List of tour offerts
        /// </summary>
        [JsonProperty("offers")]
        public TravelOffer[] Offers { get; set; }

        /// <summary>
        /// Meta-data regarding pagination.
        /// </summary>
        [JsonProperty("pages")]
        public Pages Pages { get; set; }
    }
}
