using MediatR;
using MedicineManaging.Domain.Entities.Clinics;

namespace MedicineManaging.Infrastructure.MediatR.Clinics.Queries;

public record GetClinicsByPageQuery(int Page, int PageSize) : IRequest<IEnumerable<Clinic>>;
public record GetClinicByNameQuery(string Name) : IRequest<Clinic>;
public record GetClinicsQuery() : IRequest<IEnumerable<Clinic>>;
public record SearchClinicsQuery(string Name) : IRequest<IEnumerable<Clinic>>;