using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class ClinicMutation
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicMutation(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;
        }

        public Task<bool> CreateClinicAsync(Clinic clinic) =>
            TaskExecutor.GetResultAsync(() => _clinicRepository.AddAsync(clinic));

        public Task<bool> UpdateClinicAsync(string id, Clinic clinic) =>
            TaskExecutor.GetResultAsync(() => _clinicRepository.UpdateAsync(id, clinic));

        public Task<bool> RemoveClinicAsync(string id) =>
            TaskExecutor.GetResultAsync(() => _clinicRepository.DeleteAsync(id));
    }
}
