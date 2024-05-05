namespace vgt_api.Models.Common
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Describes single tour offer including its availability.
    /// </summary>
    public partial class TravelOffer
    {
        [JsonProperty("availability", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Availability { get; set; }

        [JsonProperty("date")]
        public TravelDateRange Date { get; set; }

        [JsonProperty("destination")]
        public TravelLocation Destination { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("origin")]
        public TravelLocation Origin { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public double? Rating { get; set; }
    }
}
