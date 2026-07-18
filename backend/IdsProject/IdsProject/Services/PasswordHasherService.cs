using System.Security.Cryptography;
using System.Text;

namespace IdsProject.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            string hashedInputPassword = HashPassword(password);
            return hashedInputPassword == passwordHash;
        }
    }
}