using MediatR;
using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Infrastructure.MediatR.Clinics.Queries;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class ClinicQuery
    {
        public Task<IEnumerable<Clinic>> GetClinicsAsync([Service] IMediator mediator) =>
            mediator.Send(new GetClinicsQuery());

        public Task<Clinic> GetClinicByNameAsync(string name, [Service] IMediator mediator) =>
            mediator.Send(new GetClinicByNameQuery(name));

        public Task<IEnumerable<Clinic>> GetClinicsByPageAsync(int page,
                                                               int pageSize,
                                                               [Service] IMediator mediator) =>
            mediator.Send(new GetClinicsByPageQuery(page, pageSize));
    }
}
