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
    public class Filters
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
        public Participants Participants { get; set; }

        /// <summary>
        /// Date range of available tours.
        /// </summary>
        [JsonProperty("dates")]
        public TravelDateRange Dates { get; set; }
        
        public static Filters GetExample()
        {
            return new Filters
            {
                Dates = new TravelDateRange
                {
                    Start = "01-05-2024",
                    End = "01-01-2025"
                },
                Destinations = new TravelLocation[]
                {
                    new TravelLocation
                    {
                        Id = "1",
                        Label = "United States",
                        Locations = new TravelLocation[]
                        {
                            new TravelLocation
                            {
                                Id = "2",
                                Label = "New York"
                            }
                        }
                    }
                },
                Origins = new TravelLocation[]
                {
                    new TravelLocation
                    {
                        Id = "3",
                        Label = "Poland",
                        Locations = new TravelLocation[]
                        {
                            new TravelLocation
                            {
                                Id = "6",
                                Label = "Warsaw"
                            }
                        }
                    }
                },
                Participants = new Participants
                {
                    Max = 15,
                    Min = 1,
                    Options = new ParticipantOption[]
                    {
                        new ParticipantOption()
                        {
                            Id = "1",
                            Label = "Adults",
                            Min = 1,
                            Max = 10
                        },
                        new ParticipantOption()
                        {
                            Id = "2",
                            Label = "Children",
                            Min = 0,
                            Max = 5
                        }
                    }
                }
            };
        }
    }


}
