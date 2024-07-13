using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwtTokenService;
        public AuthController(IUserService userService, JwtTokenService jwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.Authenticate(model.UserName, model.Password);
            if (user == null)
                return BadRequest("Podano błędne dane lub hasło");

            var token = _jwtTokenService.GenerateJwtToken(user);
            return Ok(new
            {
                Token = token 
            });
        }
    }
}
