using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data.Config;
using MongoDB.Driver;

namespace MedicineManaging.Infrastructure.Data.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<Clinic> _clinics;

        public ClinicRepository(MongoContext context)
        {
            _context = context;

            _clinics = _context.Database.GetCollection<Clinic>("clinics");
        }

        public Task AddAsync(Clinic clinic) =>
             _clinics.InsertOneAsync(clinic);

        public Task DeleteAsync(string clinicId) =>
            _clinics.DeleteOneAsync(x => x.Id == clinicId);

        public Task DeleteAsync(Clinic clinic) =>
            _clinics.DeleteOneAsync(x => x.Equals(clinic));

        public Task<IEnumerable<Clinic>> FindAllAsync() =>
            Task.FromResult(_clinics.AsQueryable().ToEnumerable());

        public Task<Clinic> FindByIdAsync(string clinicId) =>
            _clinics.Find(x => x.Id == clinicId).FirstOrDefaultAsync();

        public Task<Clinic> FindByNameAsync(string name) =>
            _clinics.Find(x => x.Name == name).FirstOrDefaultAsync();

        public Task<IEnumerable<Clinic>> FindByPageAsync(int page = 1, int pageSize = 5) =>
            Task.FromResult(_clinics.AsQueryable().Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable());

        public Task<IEnumerable<Clinic>> SearchAsync(string name) =>
            Task.FromResult(_clinics.AsQueryable().Where(x => x.Name.Contains(name)).AsEnumerable());

        public Task UpdateAsync(string clinicId, Clinic clinic) =>
            _clinics.ReplaceOneAsync(x => x.Id == clinicId, clinic);
    }
}
