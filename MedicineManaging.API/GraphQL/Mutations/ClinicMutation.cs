using MediatR;
using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Infrastructure.MediatR.Clinics.Commands;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class ClinicMutation
    {
        private readonly IMediator _mediator;

        public ClinicMutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<bool> CreateClinicAsync(AddClinicModel clinic) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new AddClinicCommand(clinic)));

        public Task<bool> UpdateClinicAsync(string id, Clinic clinic) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new UpdateClinicCommand(id, clinic)));

        public Task<bool> RemoveClinicAsync(string id) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new DeleteClinicByIdCommand(id)));
    }
}
