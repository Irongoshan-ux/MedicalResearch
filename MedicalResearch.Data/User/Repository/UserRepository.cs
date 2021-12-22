using MedicalResearch.Core.User.Repository;

namespace MedicalResearch.Data.User.Repository
{
    public class UserRepository
    {
        readonly IUserRepository repository;

        public UserRepository(IUserRepository repository)
        {
            this.repository = repository;
        }
    }
}
