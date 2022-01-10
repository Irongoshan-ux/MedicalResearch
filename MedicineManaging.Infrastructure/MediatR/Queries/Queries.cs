using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Queries;

public record GetMedicineByIdQuery(string Id) : IRequest<Medicine>;
public record GetMedicinesByPageQuery(int page, int pageSize) : IRequest<IEnumerable<Medicine>>;
public record GetMedicinesQuery() : IRequest<IEnumerable<Medicine>>;