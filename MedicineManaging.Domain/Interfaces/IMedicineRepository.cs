﻿using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Domain.Interfaces
{
    public interface IMedicineRepository
    {
        Task<Medicine> FindByIdAsync(string medicineId);
        Task AddAsync(Medicine medicine);
        Task<IEnumerable<Medicine>> FindAllAsync();
        Task<IEnumerable<Medicine>> FindByPageAsync(int page = 1, int pageSize = 5);
        Task DeleteAsync(string medicineId);
        Task DeleteAsync(Medicine medicine);
        Task UpdateAsync(string medicineId, Medicine medicine);
        Task<IEnumerable<Medicine>> SearchAsync(MedicineType? medicineType, Container? container);
    }
}
