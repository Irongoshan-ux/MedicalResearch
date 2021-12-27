using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.Data.Config;
using MongoDB.Driver;
using System.Linq.Expressions;

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
            _medicines.DeleteOneAsync(x => x.Id == medicine.Id);

        public Task DeleteAsync(string medicineId) =>
            DeleteAsync(x => x.Id == medicineId);

        public Task DeleteAsync(Expression<Func<Medicine, bool>> expression) =>
            _medicines.DeleteOneAsync(expression);

        public Task<IEnumerable<Medicine>> FindAllAsync() =>
            Task.FromResult(_medicines.AsQueryable().ToEnumerable());

        public Task<Medicine> FindByIdAsync(string medicineId) =>
            _medicines.Find(x => x.Id == medicineId).FirstOrDefaultAsync();

        public Task UpdateAsync(string medicineId, Medicine medicine) =>
            _medicines.ReplaceOneAsync(x => x.Id == medicineId, medicine);
    }
}
