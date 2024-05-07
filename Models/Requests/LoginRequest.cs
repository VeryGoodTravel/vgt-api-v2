namespace vgt_api.Models.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Login request containing user data required to authenticate.
    /// </summary>
    public class LoginRequest
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
