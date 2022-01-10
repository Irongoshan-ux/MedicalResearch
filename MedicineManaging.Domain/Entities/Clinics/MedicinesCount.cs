using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Domain.Entities.Clinics
{
    public class MedicinesCountForClinic
    {
        public ICollection<Medicine>? Medicines { get; set; }
        public int Count { get; set; }
    }
}
