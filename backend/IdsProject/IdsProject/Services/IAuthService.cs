using IdsProject.DTOs;
using IdsProject.Models;

namespace IdsProject.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
        Task<UserDto?> RegisterAsync(RegisterRequestDto request);
        Task<string?> ForgotPasswordAsync(ForgotPasswordRequestDto request);
        Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request);
    }
}
