using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace BarberApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthService _authService;
       
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            try
            {
                var result = await _authService.Register(model);
                return Ok(result);
            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            try
            {
                var token = await _authService.Login(model);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
