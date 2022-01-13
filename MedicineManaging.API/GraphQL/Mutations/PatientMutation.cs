using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class PatientMutation
    {
        private readonly IPatientRepository _patientRepository;

        public PatientMutation(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<bool> CreatePatientAsync(Patient patient) =>
            TaskExecutor.GetResultAsync(() => _patientRepository.AddAsync(patient));

        public Task<bool> UpdatePatientAsync(int id, Patient patient) =>
            TaskExecutor.GetResultAsync(() => _patientRepository.UpdateAsync(id, patient));

        public Task<bool> RemovePatientAsync(int id) =>
            TaskExecutor.GetResultAsync(() => _patientRepository.DeleteAsync(id));
    }
}
