using AutoMapper;
using UserManaging.Domain.Interfaces;

namespace UserManaging.Domain.Entities.Users
{
    public class UserAuthenticationService : IUserAuthenticaionService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
        }

        public Task<User> AuthenticateAsync(string email, string passwordHash,CancellationToken cancellationToken) =>
            _userRepository.GetUserByCredentialsAsync(email, passwordHash, cancellationToken);
    }
}
