using Microsoft.AspNetCore.Mvc;

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
    public class OfferRequest
    {
        [JsonProperty("offer_id")]
        [FromQuery(Name = "offer_id")]
        public string OfferId { get; set; }
    }
}
