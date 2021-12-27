using MedicineManaging.Domain.Entities.Medicines;
using System.Linq.Expressions;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Medicine> FindByIdAsync(string medicineId);
        Task AddAsync(Medicine medicine);
        Task<IEnumerable<Medicine>> FindAllAsync();
        Task DeleteAsync(string medicineId);
        Task DeleteAsync(Expression<Func<Medicine, bool>> expression);
        Task UpdateAsync(string medicineId, Medicine medicine);
    }
}
