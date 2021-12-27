using Microsoft.AspNetCore.Identity;
using UserManaging.Domain.Entities.Images;

namespace UserManaging.Domain.Entities.Users
{
    public class User : IdentityUser
    {
        public ICollection<Image>? Images { get; set; }
    }
}