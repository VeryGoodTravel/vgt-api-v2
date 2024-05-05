namespace vgt_api.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Response to login request containing JWT token
    /// </summary>
    public partial class Login
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
