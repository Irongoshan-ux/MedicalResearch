using MedicineManaging.Domain.Entities.Medicines;
using System.Linq.Expressions;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Medicine> FindByIdAsync(string medicineId);
        Task AddAsync(Medicine medicine);
        Task<IEnumerable<Medicine>> FindAllAsync();
        Task<IEnumerable<Medicine>> FindByPageAsync(int page = 1, int pageSize = 5);
        Task DeleteAsync(string medicineId);
        Task DeleteAsync(Expression<Func<Medicine, bool>> expression);
        Task UpdateAsync(string medicineId, Medicine medicine);
    }
}
