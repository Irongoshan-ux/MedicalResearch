using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class MedicineMutation
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineMutation(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<bool> CreateMedicineAsync(Medicine medicine) =>
            TaskExecutor.GetResultAsync(() => _medicineRepository.AddAsync(medicine));

        public Task<bool> UpdateMedicineAsync(string id, Medicine medicine) =>
            TaskExecutor.GetResultAsync(() => _medicineRepository.UpdateAsync(id, medicine));

        public Task<bool> RemoveMedicineAsync(string id) =>
            TaskExecutor.GetResultAsync(() => _medicineRepository.DeleteAsync(id));
    }
}
