namespace vgt_api.Models.Requests
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Request to get details of specific offer by id.
    /// </summary>
    public partial class GetOfferDetails
    {
        [JsonProperty("offer_id")]
        public string OfferId { get; set; }
    }
}
