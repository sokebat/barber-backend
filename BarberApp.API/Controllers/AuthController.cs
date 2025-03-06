using BarberApp.Application.Interface;
using BarberApp.Domain;

using Microsoft.AspNetCore.Mvc;

namespace BarberApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, [FromQuery] string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { message = "User data and password are required." });
            }

            try
            {
                return Ok(new { message = "User registered successfully!", registeredUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest(new { message = "Email and password are required." });
            }

            try
            {
                var token = await _authService.Login(email, password);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
        }
    }
}
