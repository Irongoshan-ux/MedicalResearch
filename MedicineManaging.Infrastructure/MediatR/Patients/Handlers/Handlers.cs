using MediatR;
using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Patients.Commands;
using MedicineManaging.Infrastructure.MediatR.Patients.Queries;

namespace MedicineManaging.Infrastructure.MediatR.Patients.Handlers;

public class RegisterPatientHandler : IRequestHandler<RegisterPatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;

    public RegisterPatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Unit> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient()
        {
            Number = request.NewPatient.Number,
            BirthDay = request.NewPatient.BirthDay,
            Sex = request.NewPatient.Sex
        };

        await _patientRepository.AddAsync(patient);

        return Unit.Value;
    }
}

public class AddPatientHandler : IRequestHandler<AddPatientCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;

    public AddPatientHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Unit> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        await _patientRepository.AddAsync(request.Patient);

        return Unit.Value;
    }
}

public class DeletePatientByIdHandler : IRequestHandler<DeletePatientByIdCommand, Unit>
{
    private readonly IPatientRepository _patientRepository;

    public DeletePatientByIdHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Unit> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
    {
        await _patientRepository.DeleteAsync(request.Id);

        return Unit.Value;
    }
}

public class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, Patient>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientByIdHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Task<Patient> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken) =>
        _patientRepository.FindByIdAsync(request.Id);
}

public class GetPatientsByPageHandler : IRequestHandler<GetPatientsByPageQuery, IEnumerable<Patient>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientsByPageHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Task<IEnumerable<Patient>> Handle(GetPatientsByPageQuery request, CancellationToken cancellationToken) =>
        _patientRepository.FindByPageAsync(request.Page, request.PageSize);
}

public class GetPatientsHandler : IRequestHandler<GetPatientsQuery, IEnumerable<Patient>>
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientsHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Task<IEnumerable<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken) =>
        _patientRepository.FindAllAsync();
}

public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, Unit>
{
    private readonly IPatientRepository _PatientRepository;

    public UpdatePatientHandler(IPatientRepository PatientRepository)
    {
        _PatientRepository = PatientRepository;
    }

    public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        await _PatientRepository.UpdateAsync(request.Id, request.Patient);

        return Unit.Value;
    }
}