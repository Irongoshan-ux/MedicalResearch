using MediatR;
using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Infrastructure.MediatR.Patients.Commands;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class PatientMutation
    {
        private readonly IMediator _mediator;

        public PatientMutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<bool> RegisterPatientAsync(RegisterPatientModel patientModel) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new RegisterPatientCommand(patientModel)));

        public Task<bool> CreatePatientAsync(Patient patient) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new AddPatientCommand(patient)));

        public Task<bool> UpdatePatientAsync(int id, Patient patient) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new UpdatePatientCommand(id, patient)));

        public Task<bool> RemovePatientAsync(int id) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new DeletePatientByIdCommand(id)));
    }
}
