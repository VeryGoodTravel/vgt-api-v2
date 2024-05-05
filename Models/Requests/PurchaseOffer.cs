namespace vgt_api.Models.Requests
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Request to buy specific offer by a user.
    /// </summary>
    public partial class PurchaseOffer
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("offer_id")]
        public string OfferId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}