using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class ClinicQuery
    {
        public Task<IEnumerable<Clinic>> GetClinicsAsync([Service] IClinicRepository clinicRepository) =>
            clinicRepository.FindAllAsync();

        public Task<Clinic> GetMedicineByNameAsync(string name, [Service] IClinicRepository clinicRepository) =>
            clinicRepository.FindByNameAsync(name);

        public Task<IEnumerable<Clinic>> GetClinicsByPageAsync(int page,
                                                               int pageSize,
                                                               [Service] IClinicRepository clinicRepository) =>
            clinicRepository.FindByPageAsync(page, pageSize);
    }
}
