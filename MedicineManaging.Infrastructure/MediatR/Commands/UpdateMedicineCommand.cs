using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Commands
{
    public record UpdateMedicineCommand(string Id, Medicine Medicine) : IRequest;
}
