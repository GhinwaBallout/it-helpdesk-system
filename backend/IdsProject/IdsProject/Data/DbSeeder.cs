using IdsProject.Models;
using IdsProject.Services;
using Microsoft.EntityFrameworkCore;

namespace IdsProject.Data
{
    //DbSeeder is used to initialize the database with the first admin account for testing and first access
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            await context.Database.MigrateAsync();

            var adminRole = await context.Roles
                .FirstOrDefaultAsync(r => r.Name == RoleNames.Admin);

            if (adminRole == null)
            {
                return;
            }

            bool adminExists = await context.Users
                .AnyAsync(u => u.Email == "ghinwaballout0@gmail.com");

            if (!adminExists)
            {
                var adminUser = new User
                {
                    Name = "System Admin",
                    Email = "ghinwaballout0@gmail.com",
                    PasswordHash = passwordHasher.HashPassword("Admin123!"),
                    RoleId = adminRole.id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}