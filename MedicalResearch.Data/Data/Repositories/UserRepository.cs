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

        public UserRepository(ApplicationDbContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Users.AddAsync(user, cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            await _context.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Remove(user);
         
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            return _mapper.Map<User>(user);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(x => x.NormalizedUserName == normalizedUserName)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<User>(user);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x == user, cancellationToken);
         
            return foundUser.NormalizedUserName;
        }

        public async Task<User> GetUserByCredentialsAsync(string email, string passwordHash,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => (x.Email == email) && (x.PasswordHash == passwordHash),
                    cancellationToken);

            return _mapper.Map<User>(user);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

            return _mapper.Map<User>(user);
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x == user, cancellationToken);

            return foundUser.Id;
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x == user, cancellationToken);

            return foundUser.UserName;
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x == user, cancellationToken);

            if (foundUser != null)
            {
                foundUser.NormalizedUserName = normalizedName;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            var foundUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x == user, cancellationToken);

            if (foundUser != null)
            {
                foundUser.UserName = userName;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Update(user);

            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }
    }
}
