using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using vgt_api.Models.Common;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("Login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public Envelope<LoginResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!Regex.IsMatch(request.Username, "^s[0-9]{6}$") || request.Password != request.Username.Substring(1))
                    return Envelope<LoginResponse>.Error("Ooops, wrong username or password");
            
                string token = JwtHandler.GenerateJwtToken(request.Username);
                return Envelope<LoginResponse>.Ok(new LoginResponse
                {
                    Token = token
                });
            } catch (Exception e)
            {
                return Envelope<LoginResponse>.Error(e.Message);
            }
        }
    }
}
