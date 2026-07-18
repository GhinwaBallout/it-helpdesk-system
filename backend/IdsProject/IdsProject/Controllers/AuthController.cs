using IdsProject.DTOs;
using IdsProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await authService.LoginAsync(request);

            if (result == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            var result = await authService.RegisterAsync(request);

            if (result == null)
            {
                return BadRequest("Email already exists or role is invalid.");
            }

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto request)
        {
            var token = await authService.ForgotPasswordAsync(request);

            if (token == null)
            {
                return NotFound("User not found.");
            }

            return Ok(new
            {
                message = "Password reset token generated successfully.",
                token = token
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
        {
            var result = await authService.ResetPasswordAsync(request);

            if (!result)
            {
                return BadRequest("Invalid or expired token.");
            }

            return Ok("Password reset successfully.");
        }
    }
}