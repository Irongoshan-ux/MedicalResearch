using MediatR;

namespace MedicineManaging.Infrastructure.MediatR.Commands
{
    public record DeleteMedicineByIdCommand(string Id) : IRequest;
}
