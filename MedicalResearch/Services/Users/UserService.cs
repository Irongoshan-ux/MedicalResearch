using AutoMapper;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(User user, CancellationToken cancellationToken)
        {
            user.PasswordHash = Utilities.Utility.Encrypt(user.PasswordHash);

            try
            {
                await _userRepository.CreateAsync(user, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string userEmail, CancellationToken cancellationToken)
        {
            var user = await GetUserRequiredInfoAsync(await FindByEmailAsync(userEmail, cancellationToken),
                cancellationToken);

            var result = await _userRepository.DeleteAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDTO>> FindAllAsync(CancellationToken cancellationToken)
        {
            var users = await _userRepository.FindAllAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            return _mapper.Map<UserDTO>(user);
        }

        public Task<User> FindEntityByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _userRepository.GetUserByEmailAsync(email, cancellationToken);
        }

        public async Task<UserDTO> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(userId, cancellationToken);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByNameAsync(normalizedUserName, cancellationToken);

            return _mapper.Map<UserDTO>(user);
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

        public async Task SetNormalizedUserNameAsync(UserDTO userDto, string normalizedName,
            CancellationToken cancellationToken)
        {
            var user = await GetUserRequiredInfoAsync(userDto, cancellationToken);

            await _userRepository.SetNormalizedUserNameAsync(user, normalizedName, cancellationToken);
        }

        public async Task SetUserNameAsync(UserDTO userDto, string userName, CancellationToken cancellationToken)
        {
            var user = await GetUserRequiredInfoAsync(userDto, cancellationToken);

            await _userRepository.SetUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<bool> UpdateAsync(UserDTO userDto, CancellationToken cancellationToken)
        {
            var user = await GetUserRequiredInfoAsync(userDto, cancellationToken);

            var result = await _userRepository.UpdateAsync(user, cancellationToken);

            return result.Succeeded;
        }

        public async Task<bool> IsInRoleAsync(UserDTO user, string role)
        {
            var userFromDb = await GetUserRequiredInfoAsync(user, CancellationToken.None);

            return await _userRepository.IsInRoleAsync(userFromDb, role);
        }

        private async Task<User> GetUserRequiredInfoAsync(UserDTO userDto, CancellationToken cancellationToken)
        {
            var userFromDb = await _userRepository.GetUserByEmailAsync(userDto.Email, cancellationToken);

            var user = _mapper.Map<User>(userDto);

            user.ConcurrencyStamp = userFromDb?.ConcurrencyStamp;
            user.Id = userFromDb?.Id;
            
            if (user.Images is null) user.Images = userFromDb?.Images;

            return user;
        }
    }
}