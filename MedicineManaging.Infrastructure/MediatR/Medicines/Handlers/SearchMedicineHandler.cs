using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Handlers
{
    public class SearchMedicineHandler : IRequestHandler<SearchMedicinesQuery, IEnumerable<Medicine>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public SearchMedicineHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<IEnumerable<Medicine>> Handle(SearchMedicinesQuery request, CancellationToken cancellationToken) =>
            _medicineRepository.SearchAsync(request.MedicineType, request.Container);
    }
}
