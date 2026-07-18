using IdsProject.Models;
using Microsoft.EntityFrameworkCore;
namespace IdsProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.id);

                entity.Property(r => r.Name)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(r => r.Name)
                      .IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.id);

                entity.Property(u => u.Name)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.Property(u => u.Email)
                      .HasMaxLength(150)
                      .IsRequired();

                entity.HasIndex(u => u.Email)
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.HasOne(u => u.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PasswordResetToken>(entity =>
            {
                entity.ToTable("PasswordResetTokens");
                entity.HasKey(t => t.id);

                entity.Property(t => t.Token)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.HasIndex(t => t.Token)
                      .IsUnique();

                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            //This inserts the four roles automatically when the database is created.
            modelBuilder.Entity<Role>().HasData(
                new Role { id = 1, Name = RoleNames.Admin },
                new Role { id = 2, Name = RoleNames.Manager },
                new Role { id = 3, Name = RoleNames.ITAgent },
                new Role { id = 4, Name = RoleNames.Employee }
            );
        }
    }
}
