using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using vgt_api.Models.Envelope;
using vgt_api.Models.Requests;
using vgt_api.Models.Responses;
using vgt_api.Services;

namespace vgt_api.Controllers
{
    [ApiController]
    [Route("api/Login")]
    public partial class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly JwtService _jwtService;

        public LoginController(ILogger<LoginController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost]
        public Envelope<LoginResponse> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (!UsernameRegex().IsMatch(request.Login) || 
                    request.Password != $"password{request.Login[1..]}")
                    return Envelope<LoginResponse>.Error("Ooops, wrong username or password");
            
                string token = _jwtService.GenerateJwtToken(request.Login);
                LoginResponse response = new LoginResponse(token);
                return Envelope<LoginResponse>.Ok(response);
            } catch (Exception e)
            {
                return Envelope<LoginResponse>.Error(e.Message);
            }
        }

        [GeneratedRegex("^s[0-9]{6}$")]
        private static partial Regex UsernameRegex();
    }
}
