using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Handlers
{
    public class GetMedicinesByPageHandler : IRequestHandler<GetMedicinesByPageQuery, IEnumerable<Medicine>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicinesByPageHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<IEnumerable<Medicine>> Handle(GetMedicinesByPageQuery request, CancellationToken cancellationToken) =>
            _medicineRepository.FindByPageAsync(request.Page, request.PageSize);
    }
}
