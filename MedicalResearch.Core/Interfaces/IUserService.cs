using UserManaging.Domain.Entities.Users;

namespace UserManaging.Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken);
        Task SetUserNameAsync(UserDTO user, string userName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(UserDTO user, string normalizedName, CancellationToken cancellationToken);
        Task<bool> CreateAsync(User user, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(UserDTO user, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(string userEmail, CancellationToken cancellationToken);
        Task<IEnumerable<UserDTO>> FindAllAsync(CancellationToken cancellationToken);
        Task<UserDTO> FindByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
        Task<User> FindEntityByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> IsInRoleAsync(UserDTO user, string role);
    }
}
