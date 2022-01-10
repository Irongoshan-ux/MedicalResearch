using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Handlers
{
    public class GetMedicinesByPageHandler : IRequestHandler<GetMedicinesByPageQuery, IEnumerable<Medicine>>
    {
        private readonly IMedicineRepository _medicineRepository;

        public GetMedicinesByPageHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public Task<IEnumerable<Medicine>> Handle(GetMedicinesByPageQuery request, CancellationToken cancellationToken) =>
            _medicineRepository.FindByPageAsync(request.page, request.pageSize);
    }
}
