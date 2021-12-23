using UserManaging.Domain.Entities.Users;

namespace UserManaging.Domain.Interfaces
{
    public interface IUserAuthenticaionService
    {
        Task<User> AuthenticateAsync(string email, string password, CancellationToken cancellationToken);
    }
}
