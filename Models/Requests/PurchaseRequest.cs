using Microsoft.AspNetCore.Mvc;

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
    public class PurchaseRequest
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("offer_id")]
        public string offer_id { get; set; }
        
        public string OfferId => offer_id;

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}