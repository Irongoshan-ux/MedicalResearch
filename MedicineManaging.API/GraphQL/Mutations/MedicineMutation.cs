using MediatR;
using MedicineManaging.API.Utilities.DotNetTaskExecutors;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Infrastructure.MediatR.Medicines.Commands;

namespace MedicineManaging.API.GraphQL.Mutations
{
    public class MedicineMutation
    {
        private readonly IMediator _mediator;

        public MedicineMutation(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<bool> CreateMedicineAsync(Medicine medicine) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new AddMedicineCommand(medicine)));

        public Task<bool> UpdateMedicineAsync(string id, Medicine medicine) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new UpdateMedicineCommand(id, medicine)));

        public Task<bool> RemoveMedicineAsync(string id) =>
            TaskExecutor.GetResultAsync(() => _mediator.Send(new DeleteMedicineByIdCommand(id)));
    }
}
