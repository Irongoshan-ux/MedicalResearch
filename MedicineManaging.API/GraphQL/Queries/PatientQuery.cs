using MediatR;
using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Infrastructure.MediatR.Patients.Queries;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class PatientQuery
    {
        public Task<IEnumerable<Patient>> GetPatientsAsync([Service] IMediator mediator) =>
            mediator.Send(new GetPatientsQuery());

        public Task<Patient> GetPatientByIdAsync(int id, [Service] IMediator mediator) =>
            mediator.Send(new GetPatientByIdQuery(id));

        public Task<IEnumerable<Patient>> GetPatientByPageAsync(int page, int pageSize, [Service] IMediator mediator) =>
            mediator.Send(new GetPatientsByPageQuery(page, pageSize));
    }
}
