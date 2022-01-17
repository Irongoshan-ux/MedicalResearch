using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data.Config;
using MongoDB.Driver;

namespace MedicineManaging.Infrastructure.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<Patient> _patients;

        public PatientRepository(MongoContext context)
        {
            _context = context;

            _patients = _context.Database.GetCollection<Patient>("patients");
        }

        public Task AddAsync(Patient patient) =>
            _patients.InsertOneAsync(patient);

        public Task DeleteAsync(int id) =>
            _patients.DeleteOneAsync(x => x.Number == id);

        public Task DeleteAsync(Patient patient) =>
            _patients.DeleteOneAsync(x => x.Equals(patient));

        public Task<IEnumerable<Patient>> FindAllAsync() =>
            Task.FromResult(_patients.AsQueryable().ToEnumerable());

        public Task<Patient> FindByIdAsync(int id) =>
            _patients.Find(x => x.Number == id).FirstOrDefaultAsync();

        public Task<IEnumerable<Patient>> FindByPageAsync(int page = 1, int pageSize = 5) =>
            Task.FromResult(_patients.AsQueryable().Skip(page - 1).Take(pageSize).AsEnumerable());

        public Task UpdateAsync(int id, Patient patient) =>
            _patients.ReplaceOneAsync(x => x.Number == id, patient);
    }
}
