using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace vgt_api.Models.Requests
{
    /// <summary>
    /// Request to get details of specific offer by id.
    /// </summary>
    public class OfferRequest
    {
        [JsonProperty("offer_id")]
        [FromQuery(Name = "offer_id")]
        public string OfferId { get; set; }
    }
}
