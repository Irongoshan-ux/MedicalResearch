﻿using Microsoft.AspNetCore.Identity;
using UserManaging.Domain.Entities.Users;

namespace UserManaging.Domain.Interfaces
{
    public interface IUserRepository : IUserStore<User>
    {
        Task<User> GetUserByCredentialsAsync(string email, string passwordHash, CancellationToken cancellationToken);
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}