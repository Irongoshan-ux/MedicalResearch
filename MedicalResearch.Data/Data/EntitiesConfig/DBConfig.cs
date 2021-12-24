using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace UserManaging.Infrastructure.Data.EntitiesConfig
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string> // Admin
                {
                    RoleId = "qwe",
                    UserId = "qwe"
                },
                new IdentityUserRole<string> // User
                {
                    RoleId = "qwerty",
                    UserId = "qwerty"
                });
        }
    }

    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole
                {
                    Id = "qwerty",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "qwe",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}
