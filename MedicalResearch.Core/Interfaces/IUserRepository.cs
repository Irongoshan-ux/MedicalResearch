using Microsoft.AspNetCore.Identity;
using UserManaging.Domain.Entities.Users;

namespace UserManaging.Domain.Interfaces
{
    public interface IUserRepository : IUserStore<User>
    {
    }
}
