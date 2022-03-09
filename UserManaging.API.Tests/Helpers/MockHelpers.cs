using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserManaging.Domain.Entities.Users;
using UserManaging.Domain.Interfaces;
using UserManaging.Infrastructure.Data.Repositories;
using UserManaging.Infrastructure.Data;

namespace UserManaging.API.Tests.Helpers
{
    internal static class MockHelpers
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserRepository>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            return mgr;
        }

        public static IUserRepository GetInMemoryUserRepository()
        {
            return new UserRepository(GetDbContext(), MockUserManager<User>().Object);
        }

        public static ApplicationDbContext GetDbContext() =>
            InMemoryDbContextGenerator.Context;
    }
}
