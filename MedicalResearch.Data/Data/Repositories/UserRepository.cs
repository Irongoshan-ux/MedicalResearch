using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;

namespace UserManaging.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private bool disposedValue;

        private IQueryable<User> UserWithImages =>
         _userManager.Users
                    .AsNoTracking()
                    .Include(x => x.Images);

        public UserRepository(ApplicationDbContext userContext, IMapper mapper, UserManager<User> userManager)
        {
            _context = userContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            return _userManager.CreateAsync(user);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            return _userManager.DeleteAsync(user);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return UserWithImages
                .Where(x => x.Id == userId)
                .FirstAsync();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return UserWithImages
                .Where(x => x.NormalizedUserName == normalizedUserName)
                .FirstAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstAsync(x => x.Email == user.Email, cancellationToken);

            return foundUser.NormalizedUserName;
        }

        public Task<User> GetUserByCredentialsAsync(string email, string passwordHash,
            CancellationToken cancellationToken)
        {
            return UserWithImages
                .FirstAsync(x => (x.Email == email) && (x.PasswordHash == passwordHash),
                    cancellationToken);
        }

        public Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return UserWithImages
                .FirstAsync(x => x.Email == email, cancellationToken);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return _userManager.GetUserIdAsync(user);
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return _userManager.GetUserNameAsync(user);
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == user.Email, cancellationToken);

            if (foundUser != null)
            {
                foundUser.NormalizedUserName = normalizedName;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return _userManager.SetUserNameAsync(user, userName);
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            return _userManager.UpdateAsync(user);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }
    }
}
