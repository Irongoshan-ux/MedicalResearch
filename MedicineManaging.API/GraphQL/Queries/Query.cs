using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class Query
    {
        public Task<IEnumerable<Medicine>> GetMedicinesAsync([Service] IMedicineRepository medicineRepository) =>
            medicineRepository.FindAllAsync();

        public Task<Medicine> GetMedicineById(string id, [Service] IMedicineRepository medicineRepository) =>
            medicineRepository.FindByIdAsync(id);
    }
}
