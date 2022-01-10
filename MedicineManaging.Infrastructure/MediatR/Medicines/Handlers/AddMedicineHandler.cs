using MediatR;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Medicines.Commands;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Handlers
{
    public class AddMedicineHandler : IRequestHandler<AddMedicineCommand, Unit>
    {
        private readonly IMedicineRepository _medicineRepository;

        public AddMedicineHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<Unit> Handle(AddMedicineCommand request, CancellationToken cancellationToken)
        {
            await _medicineRepository.AddAsync(request.Medicine);

            return Unit.Value;
        }
    }
}
