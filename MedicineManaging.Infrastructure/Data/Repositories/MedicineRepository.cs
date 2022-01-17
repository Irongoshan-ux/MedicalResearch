using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data.Config;
using MongoDB.Driver;

namespace MedicineManaging.Infrastructure.Data.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<Medicine> _medicines;

        public MedicineRepository(MongoContext context)
        {
            _context = context;

            _medicines = _context.Database.GetCollection<Medicine>("medicines");
        }

        public Task AddAsync(Medicine medicine) =>
            _medicines.InsertOneAsync(medicine);

        public Task DeleteAsync(Medicine medicine) =>
            _medicines.DeleteOneAsync(x => x.Equals(medicine));

        public Task DeleteAsync(string medicineId) =>
            _medicines.DeleteOneAsync(x => x.Id == medicineId);

        public Task<IEnumerable<Medicine>> FindAllAsync() =>
            Task.FromResult(_medicines.AsQueryable().ToEnumerable());

        public Task<Medicine> FindByIdAsync(string medicineId) =>
            _medicines.Find(x => x.Id == medicineId).FirstOrDefaultAsync();

        public Task<IEnumerable<Medicine>> FindByPageAsync(int page = 1, int pageSize = 5) =>
            Task.FromResult(_medicines.AsQueryable().Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable());

        public Task<IEnumerable<Medicine>> SearchAsync(MedicineType? medicineType, Container? container)
        {
            IQueryable<Medicine> query = _medicines.AsQueryable();

            if (medicineType is not null)
            {
                query = query.Where(x => x.Type == medicineType);
            }

            if (container is not null)
            {
                query = query.Where(x => x.Container == container);
            }

            return Task.FromResult(query.AsEnumerable());
        }

        public Task UpdateAsync(string medicineId, Medicine medicine) =>
            _medicines.ReplaceOneAsync(x => x.Id == medicineId, medicine);
    }
}
