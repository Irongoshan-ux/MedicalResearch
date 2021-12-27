using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManaging.Domain.Entities.Images;
using UserManaging.Domain.Entities.Users;
using UserManaging.Infrastructure.Data.EntitiesConfig;

namespace UserManaging.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Image> Images { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());

            InitializeUserTable(modelBuilder);
        }

        private void InitializeUserTable(ModelBuilder modelBuilder)
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            string userEmail = "user@gmail.com";
            string userPassword = "c54cd4083d0e3b7625cd3b8c652a2537"; // qwe

            User admin = new() { Id = "qwe", Email = adminEmail, UserName = "adminUser", PasswordHash = adminPassword };
            User user = new() { Id = "qwerty", Email = userEmail, UserName = "userUser", PasswordHash = userPassword };

            modelBuilder.Entity<User>().HasData(new User[] { admin, user });
            base.OnModelCreating(modelBuilder);
        }
    }
}
