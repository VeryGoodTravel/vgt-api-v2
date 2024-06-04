using Newtonsoft.Json;
using vgt_api.Models.Requests;

namespace vgt_api.Models.Common
{
    /// <summary>
    /// Describes single tour offer including its availability.
    /// </summary>
    public class TravelOffer
    {
        private static IConfigurationRoot _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        public TravelOffer()
        {
            Image = _configuration["Offer:Image"];
        }

        public TravelOffer(bool availability,
            IdFilters filters, Hotel hotel, Room? room, Flight flightTo, Flight flightFrom)
            : this(availability, filters,
                filters.Dates, hotel, room, flightTo, flightFrom) { }
        
        public TravelOffer(bool availability,
            SearchFilters filters, Hotel hotel, Room? room, Flight flightTo, Flight flightFrom)
            : this(availability, new IdFilters(filters, hotel, room, flightTo, flightFrom),
                filters.Dates, hotel, room, flightTo, flightFrom) { }
        
        private TravelOffer(bool availability, IdFilters id, TravelDateRange dates, Hotel hotel, Room? room, Flight flightTo, Flight flightFrom)
        {
            Availability = availability;
            Date = dates;
            Destination = new TravelLocation
            {
                Id = hotel.Country,
                Label = hotel.Country,
                Locations = new[]
                {
                    new TravelLocation
                    {
                        Id = hotel.City,
                        Label = hotel.City,
                    }
                }
            };
            Origin = new TravelLocation
            {
                Id = "Polska",
                Label = "Polska",
                Locations = new[]
                {
                    new TravelLocation
                    {
                        Id = flightTo.DepartureAirportCode,
                        Label = flightTo.DepartureAirportName,
                    }
                }
            };
            Id = id.ToString();
            Name = hotel.Name;
            this.Maintenance = "All inclusive";//room.Maintenance;
            
            var roomPrice = room?.Price ?? 0;
            var totalRoomPrice = roomPrice * id.Adults + 2 * roomPrice * id.Children18 + 3 * roomPrice * id.Children10 +
                             4 * roomPrice * id.Children3; 
            
            Price = new Price
            {
                Value = totalRoomPrice + flightTo.Price + flightFrom.Price,
                Currency = "zł"
            };
            this.Rating = 5;//room.Rating;
            Transportation = "Plane";
            Room = room?.Name ?? "Brak informacji";
            Image = _configuration["Offer:Image"];
        }
        
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
            return new TravelOffer
            {
                Availability = true,
                Date = new TravelDateRange
                {
                    Start = "01-05-2024",
                    End = "01-06-2024"
                },
                Destination = new TravelLocation
                {
                    Id = "10",
                    Label = "United States",
                    Locations = new[]
                    {
                        new TravelLocation
                        {
                            Id = "20",
                            Label = "New York",
                        }
                    }
                },
                Origin = new TravelLocation
                {
                    Id = "11",
                    Label = "Poland",
                    Locations = new[]
                    {
                        new TravelLocation
                        {
                            Id = "21",
                            Label = "Warsaw",
                        }
                    }
                },
                Id = guid.ToString(),
                Name = guid.ToString(),
                Maintenance = "All inclusive",
                Price = new Price
                {
                    Value = 3000,
                    Currency = "USD"
                },
                Rating = 4.5,
                Transportation = "Plane",
                Room = "Double"
            };
        }
    }
}
