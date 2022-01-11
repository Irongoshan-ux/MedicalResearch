using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class MedicineQuery
    {
        public Task<IEnumerable<Medicine>> GetMedicinesAsync([Service] IMedicineRepository medicineRepository) =>
            medicineRepository.FindAllAsync();

        public Task<Medicine> GetMedicineByIdAsync(string id, [Service] IMedicineRepository medicineRepository) =>
            medicineRepository.FindByIdAsync(id);
    }
}
