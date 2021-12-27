﻿using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Handlers
{
    public class GetMedicineByIdHandler : IRequestHandler<GetMedicineByIdQuery, Medicine>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicineByIdHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<Medicine> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken) =>
            _medicineRepository.FindByIdAsync(request.Id);
    }
}
