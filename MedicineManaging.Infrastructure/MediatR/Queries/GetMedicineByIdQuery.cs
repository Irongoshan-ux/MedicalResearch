using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Queries
{
    public record GetMedicineByIdQuery(string Id): IRequest<Medicine>;
}
