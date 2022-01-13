using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IMedicineRepository : IRepository<string, Medicine>
    {
        Task<IEnumerable<Medicine>> SearchAsync(MedicineType? medicineType, Container? container);
    }
}
