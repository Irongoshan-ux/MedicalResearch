using UserManaging.Domain.Entities.Users;

namespace UserManaging.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken);
        Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken);
        Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(User user, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(User user, CancellationToken cancellationToken);
        Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
    }
}
