using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class Mutation
    {
        private readonly IMedicineRepository _medicineRepository;

        public Mutation(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<bool> CreateMedicineAsync(Medicine medicine) =>
            GetResultAsync(() => _medicineRepository.AddAsync(medicine));

        public Task<bool> UpdateMedicineAsync(string id, Medicine medicine) =>
            GetResultAsync(() => _medicineRepository.UpdateAsync(id, medicine));

        public Task<bool> RemoveMedicineAsync(string id) =>
            GetResultAsync(() => _medicineRepository.DeleteAsync(id));

        private Task<bool> GetResultAsync(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
