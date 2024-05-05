namespace vgt_api.Models.Responses
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using vgt_api.Models.Common;

    /// <summary>
    /// Travel filters describing available options to be displayed in search bar.
    /// </summary>
    public partial class GetFilters
    {
        /// <summary>
        /// Available tour destinations
        /// </summary>
        [JsonProperty("destinations")]
        public TravelLocation[] Destinations { get; set; }

        /// <summary>
        /// Available places of departure.
        /// </summary>
        [JsonProperty("origins")]
        public TravelLocation[] Origins { get; set; }

        /// <summary>
        /// Participants types based on their age.
        /// </summary>
        [JsonProperty("participants")]
        public Participant[] Participants { get; set; }
    }

    
}
