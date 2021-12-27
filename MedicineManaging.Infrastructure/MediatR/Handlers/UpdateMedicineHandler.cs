using MediatR;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Commands;

namespace MedicineManaging.Infrastructure.MediatR.Handlers
{
    public class UpdateMedicineHandler : IRequestHandler<UpdateMedicineCommand, Unit>
    {
        private readonly IMedicineRepository _medicineRepository;

        public UpdateMedicineHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<Unit> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            await _medicineRepository.UpdateAsync(request.Id, request.Medicine);

            return Unit.Value;
        }
    }
}
