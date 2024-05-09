using Microsoft.AspNetCore.Mvc;

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
    public class SearchFilters
    {
        [JsonProperty("dates")]
        public TravelDateRange? Dates { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("destinations")]
        public string[]? Destinations { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("origins")]
        public string[]? Origins { get; set; }

        [JsonProperty("page")]
        [BindProperty(Name = "page", SupportsGet = true)]
        public int Page { get; set; }

        /// <summary>
        /// Participant type with count 0 won't be included in the request.
        /// </summary>
        [JsonProperty("participants")]
        public Dictionary<int, int>? Participants { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
