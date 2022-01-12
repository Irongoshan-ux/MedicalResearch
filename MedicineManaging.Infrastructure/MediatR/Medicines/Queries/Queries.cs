using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Queries;

public record GetMedicineByIdQuery(string Id) : IRequest<Medicine>;
public record GetMedicinesByPageQuery(int Page, int PageSize) : IRequest<IEnumerable<Medicine>>;
public record GetMedicinesQuery() : IRequest<IEnumerable<Medicine>>;
public record SearchMedicinesQuery(MedicineType? MedicineType, Container? Container) : IRequest<IEnumerable<Medicine>>;