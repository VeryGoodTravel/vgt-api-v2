namespace vgt_api.Models.Responses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Describes data returned on successful offer purchase.
    /// </summary>
    public partial class PurchaseResponse
    {
        /// <summary>
        /// Description why transaction failed.
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// Information whether transaction was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}