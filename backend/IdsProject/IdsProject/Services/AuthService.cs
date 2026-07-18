using IdsProject.Data;
using IdsProject.DTOs;
using IdsProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IdsProject.Services
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext context;
		private readonly IPasswordHasher passwordHasher;
		private readonly IJwtTokenService jwtTokenService;

		public AuthService(
			AppDbContext context,
			IPasswordHasher passwordHasher,
			IJwtTokenService jwtTokenService)
		{
			this.context = context;
			this.passwordHasher = passwordHasher;
			this.jwtTokenService = jwtTokenService;
		}

		public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
		{
			var user = await context.Users
				.Include(u => u.Role)
				.FirstOrDefaultAsync(u => u.Email == request.Email);

			if (user == null)
			{
				return null;
			}

			if (!user.IsActive)
			{
				return null;
			}

			bool isPasswordValid = passwordHasher.VerifyPassword(
				request.Password,
				user.PasswordHash);

			if (!isPasswordValid)
			{
				return null;
			}

			string token = jwtTokenService.GenerateToken(user);

			return new LoginResponseDto
			{
				Token = token,
				ExpiresAt = DateTime.UtcNow.AddHours(1),
				User = new UserDto
				{
					id = user.id,
					Name = user.Name,
					Email = user.Email,
					Role = user.Role?.Name ?? ""
				}
			};
		}

		public async Task<UserDto?> RegisterAsync(RegisterRequestDto request)
		{
			bool emailExists = await context.Users
				.AnyAsync(u => u.Email == request.Email);

			if (emailExists)
			{
				return null;
			}

			var role = await context.Roles
				.FirstOrDefaultAsync(r => r.Name == request.Role);

			if (role == null)
			{
				return null;
			}

			var user = new User
			{
				Name = request.Name,
				Email = request.Email,
				PasswordHash = passwordHasher.HashPassword(request.Password),
				RoleId = role.id,
				CreatedAt = DateTime.UtcNow,
				IsActive = true
			};

			context.Users.Add(user);
			await context.SaveChangesAsync();

			return new UserDto
			{
				id = user.id,
				Name = user.Name,
				Email = user.Email,
				Role = role.Name
			};
		}

		public async Task<string?> ForgotPasswordAsync(ForgotPasswordRequestDto request)
		{
			var user = await context.Users
				.FirstOrDefaultAsync(u => u.Email == request.Email);

			if (user == null)
			{
				return null;
			}

			string token = Guid.NewGuid().ToString();

			var resetToken = new PasswordResetToken
			{
				UserId = user.id,
				Token = token,
				ExpiresAt = DateTime.UtcNow.AddMinutes(15),
				Used = false,
				CreatedAt = DateTime.UtcNow
			};

			context.PasswordResetTokens.Add(resetToken);
			await context.SaveChangesAsync();

			return token;
		}

		public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto request)
		{
			var resetToken = await context.PasswordResetTokens
				.FirstOrDefaultAsync(t =>
					t.Token == request.Token &&
					t.Used == false &&
					t.ExpiresAt > DateTime.UtcNow);

			if (resetToken == null)
			{
				return false;
			}

			var user = await context.Users
				.FirstOrDefaultAsync(u => u.id == resetToken.UserId);

			if (user == null)
			{
				return false;
			}

			user.PasswordHash = passwordHasher.HashPassword(request.NewPassword);
			resetToken.Used = true;

			await context.SaveChangesAsync();

			return true;
		}
	}
}