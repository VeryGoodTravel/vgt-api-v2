using Microsoft.AspNetCore.Mvc;
using vgt_api.Models.Requests.Flights;
using vgt_api.Models.Requests.Hotels;

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
        public TravelDateRange Dates { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("destinations")]
        public List<string> Destinations { get; set; }

        /// <summary>
        /// If none provided, it means 'any'.
        /// </summary>
        [JsonProperty("origins")]
        public List<string> Origins { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        /// <summary>
        /// Participant type with count 0 won't be included in the request.
        /// </summary>
        [JsonProperty("participants")]
        public Dictionary<int, int> Participants { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public HotelsRequest ToHotelsRequest()
        {
            return new HotelsRequest()
            {
                Dates = Dates,
                Cities = Destinations,
                Participants = Participants
            };
        }
    }
}
