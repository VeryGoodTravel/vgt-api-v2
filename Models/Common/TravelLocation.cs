namespace vgt_api.Models.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes travel location, both origins and destinations.
    /// </summary>
    public class TravelLocation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("locations", NullValueHandling = NullValueHandling.Ignore)]
        public TravelLocation[] Locations { get; set; }
        
        public static TravelLocation GetExample()
        {
            return new TravelLocation
            {
                Id = "1",
                Label = "Germany",
                Locations = new TravelLocation[]
                {
                    new TravelLocation
                    {
                        Id = "2",
                        Label = "Berlin",
                    },
                    new TravelLocation
                    {
                        Id = "5",
                        Label = "Munich",
                    }
                }
            };
        }
    }
}
