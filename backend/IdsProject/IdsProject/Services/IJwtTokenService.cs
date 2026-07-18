using IdsProject.Models;

namespace IdsProject.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}