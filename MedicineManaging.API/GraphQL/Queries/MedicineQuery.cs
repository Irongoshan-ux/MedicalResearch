using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;

namespace MedicineManaging.API.GraphQL.Queries
{
    public class MedicineQuery
    {
        public Task<IEnumerable<Medicine>> GetMedicinesAsync([Service] IMediator mediator) =>
            mediator.Send(new GetMedicinesQuery());

        public Task<Medicine> GetMedicineByIdAsync(string id, [Service] IMediator mediator) =>
            mediator.Send(new GetMedicineByIdQuery(id));
    }
}
