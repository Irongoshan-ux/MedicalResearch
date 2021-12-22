using MedicalResearch.Core.User.Domain;
using MedicalResearch.Core.User.Repository;
using MedicalResearch.Core.User.Service;

namespace MedicalResearch.BusinessLogic.User.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.CreateAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.DeleteAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userRepository.FindByIdAsync(userId, cancellationToken);
        }

        public Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userRepository.FindByNameAsync(normalizedUserName, cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return _userRepository.GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserIdAsync(user, cancellationToken);
        }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
        {
            return _userRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            return _userRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<bool> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var result = await _userRepository.UpdateAsync(user, cancellationToken);

            return result.Succeeded;
        }
    }
}