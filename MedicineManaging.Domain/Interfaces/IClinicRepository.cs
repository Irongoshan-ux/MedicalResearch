using MedicineManaging.Domain.Entities.Clinics;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IClinicRepository : IRepository<string, Clinic>
    {
        Task<Clinic> FindByNameAsync(string name);
        Task<IEnumerable<Clinic>> SearchAsync(string name);
    }
}
