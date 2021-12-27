using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.CreateAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            user = await GetUserRequiredInfoAsync(user, cancellationToken);

            var result = await _userRepository.DeleteAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserByEmailAsync(email, cancellationToken);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userRepository.FindByIdAsync(userId, cancellationToken);
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserNameAsync(user, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user = await GetUserRequiredInfoAsync(user, cancellationToken);

            await _userRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user = await GetUserRequiredInfoAsync(user, cancellationToken);

            await _userRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<bool> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            user = await GetUserRequiredInfoAsync(user, cancellationToken);

            var result = await _userRepository.UpdateAsync(user, cancellationToken);

            return result.Succeeded;
        }

        private async Task<User> GetUserRequiredInfoAsync(User user, CancellationToken cancellationToken)
        {
            var userFromDb = await FindByEmailAsync(user.Email, cancellationToken);

            user.ConcurrencyStamp = userFromDb?.ConcurrencyStamp;
            user.Id = userFromDb?.Id;

            if (user.Images is null) user.Images = userFromDb?.Images;

            return user;
        }
    }
}