using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Handlers
{
    public class GetMedicinesHandler : IRequestHandler<GetMedicinesQuery, IEnumerable<Medicine>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicinesHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<IEnumerable<Medicine>> Handle(GetMedicinesQuery request, CancellationToken cancellationToken) =>
            _medicineRepository.FindAllAsync();
    }
}
