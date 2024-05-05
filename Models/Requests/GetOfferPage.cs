namespace vgt_api.Models.Requests
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using vgt_api.Models.Common;

    /// <summary>
    /// Request to get a page of offers based on filters.
    /// </summary>
    public partial class GetOfferPage
    {
        [JsonProperty("dates")]
        public TravelDateRange Dates { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("destinations")]
        public string[] Destinations { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("origins")]
        public string[] Origins { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        /// <summary>
        /// Participant type with count 0 won't be included in the request.
        /// </summary>
        [JsonProperty("participants")]
        public Dictionary<string, object> Participants { get; set; }
    }
}
