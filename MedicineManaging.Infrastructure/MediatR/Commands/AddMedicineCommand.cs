using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Commands
{
    public record AddMedicineCommand(Medicine Medicine) : IRequest;
}
