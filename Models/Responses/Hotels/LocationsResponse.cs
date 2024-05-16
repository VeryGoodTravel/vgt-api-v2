using Newtonsoft.Json;
using vgt_api.Models.Common;

namespace vgt_api.Models.Responses.Hotels;

public class LocationsResponse
{
    // Combinations of Country and City
    // Id and Label should be the same
    [JsonProperty("locations")]
    public List<TravelLocation> Locations { get; set; }
}