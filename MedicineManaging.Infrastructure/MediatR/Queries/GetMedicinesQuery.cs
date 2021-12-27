using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Queries
{
    public record GetMedicinesQuery() : IRequest<IEnumerable<Medicine>>;
}
