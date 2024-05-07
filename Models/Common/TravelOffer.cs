﻿namespace vgt_api.Models.Common
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Describes single tour offer including its availability.
    /// </summary>
    public class TravelOffer
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

        [JsonProperty("maintenance", NullValueHandling = NullValueHandling.Ignore)]
        public string Maintenance { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("origin")]
        public TravelLocation Origin { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("rating", NullValueHandling = NullValueHandling.Ignore)]
        public double? Rating { get; set; }

        [JsonProperty("room", NullValueHandling = NullValueHandling.Ignore)]
        public string Room { get; set; }

        [JsonProperty("transportation", NullValueHandling = NullValueHandling.Ignore)]
        public string Transportation { get; set; }

        public static TravelOffer GetExample()
        {
            Guid guid = Guid.NewGuid();
            return new TravelOffer()
            {
                Availability = true,
                Date = new TravelDateRange()
                {
                    Start = "01-05-2024",
                    End = "01-06-2024"
                },
                Destination = new TravelLocation()
                {
                    Id = "10",
                    Label = "United States",
                    Locations = new[]
                    {
                        new TravelLocation()
                        {
                            Id = "20",
                            Label = "New York",
                        }
                    }
                },
                Origin = new TravelLocation()
                {
                    Id = "11",
                    Label = "Poland",
                    Locations = new[]
                    {
                        new TravelLocation()
                        {
                            Id = "21",
                            Label = "Warsaw",
                        }
                    }
                },
                Id = guid.ToString(),
                Name = guid.ToString(),
                Maintenance = "All inclusive",
                Price = new Price()
                {
                    Value = 3000,
                    Currency = "USD"
                },
                Rating = 4.5,
                Transportation = "Plane",
                Room = "Double",
                Image =
                    "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAgAAAAIAQMAAAD+wSzIAAAABlBMVEX///+/v7+jQ3Y5AAAADklEQVQI12P4AIX8EAgALgAD/aNpbtEAAAAASUVORK5CYII"
            };
        }
    }
}
