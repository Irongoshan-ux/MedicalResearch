﻿using AutoMapper;
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

        public UserRepository(ApplicationDbContext userContext, IMapper mapper, UserManager<User> userManager)
        {
            _context = userContext;
            _mapper = mapper;
            _userManager = userManager;
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
            return _userManager.FindByIdAsync(userId);
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
                .FirstOrDefaultAsync(x => x.Email == user.Email, cancellationToken);
         
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

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
