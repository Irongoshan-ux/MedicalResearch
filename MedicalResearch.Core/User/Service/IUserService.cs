using MedicalResearch.Core.User.Domain;
using MedicalResearch.Core.User.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalResearch.Core.User.Service
{
    public interface IUserService
    {
        Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken);
        Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken);
        Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken);
        Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken);
        Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken);
        Task<bool> CreateAsync(UserModel user, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(UserModel user, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(UserModel user, CancellationToken cancellationToken);
        Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken);
        Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken);
    }
}
