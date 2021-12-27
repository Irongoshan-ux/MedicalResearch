using MediatR;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Commands;

namespace MedicineManaging.Infrastructure.MediatR.Handlers
{
    public class DeleteMedicineByIdHandler : IRequestHandler<DeleteMedicineByIdCommand, Unit>
    {
        private readonly IMedicineRepository _medicineRepository;

        public DeleteMedicineByIdHandler(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<Unit> Handle(DeleteMedicineByIdCommand request, CancellationToken cancellationToken)
        {
            await _medicineRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
