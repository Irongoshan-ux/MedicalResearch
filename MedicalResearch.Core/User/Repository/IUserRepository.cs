using MedicalResearch.Core.User.Domain;
using Microsoft.AspNetCore.Identity;

namespace MedicalResearch.Core.User.Repository
{
    public interface IUserRepository : IUserStore<UserModel>
    {
    }
}
