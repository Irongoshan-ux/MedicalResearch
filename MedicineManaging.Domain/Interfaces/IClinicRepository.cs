using MedicineManaging.Domain.Entities.Clinics;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<Clinic> FindByNameAsync(string name);
        Task AddAsync(Clinic clinic);
        Task<IEnumerable<Clinic>> FindAllAsync();
        Task<IEnumerable<Clinic>> FindByPageAsync(int page = 1, int pageSize = 5);
        Task DeleteAsync(string clinicId);
        Task DeleteAsync(Clinic clinic);
        Task UpdateAsync(string clinicId, Clinic clinic);
    }
}
