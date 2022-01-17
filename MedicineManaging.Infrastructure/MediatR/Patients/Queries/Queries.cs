using MediatR;
using MedicineManaging.Domain.Entities.Patients;

namespace MedicineManaging.Infrastructure.MediatR.Patients.Queries;

public record GetPatientsByPageQuery(int Page, int PageSize) : IRequest<IEnumerable<Patient>>;
public record GetPatientsQuery() : IRequest<IEnumerable<Patient>>;
public record GetPatientByIdQuery(int Id) : IRequest<Patient>;