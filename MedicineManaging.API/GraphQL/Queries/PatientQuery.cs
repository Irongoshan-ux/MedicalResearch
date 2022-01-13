using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class PatientQuery
    {
        public Task<IEnumerable<Patient>> GetPatientsAsync([Service] IPatientRepository patientRepository) =>
            patientRepository.FindAllAsync();

        public Task<Patient> GetPatientByIdAsync(int id, [Service] IPatientRepository patientRepository) =>
            patientRepository.FindByIdAsync(id);
    }
}
