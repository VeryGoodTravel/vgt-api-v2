using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Responses.Flights;

public class DepartureAirports
{
    [JsonProperty("airports")]
    public List<Airport> Airports { get; set; }
}