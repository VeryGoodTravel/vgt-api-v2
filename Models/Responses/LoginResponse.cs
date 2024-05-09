namespace vgt_api.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Response to login request containing JWT token
    /// </summary>
    public class LoginResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        
        public LoginResponse(string token)
        {
            Token = token;
        }
    }
}
