using MediatR;
using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Domain.Interfaces;
using MedicineManaging.Infrastructure.MediatR.Clinics.Commands;
using MedicineManaging.Infrastructure.MediatR.Clinics.Queries;
using MedicineManaging.Infrastructure.MediatR.Patients.Commands;

namespace MedicineManaging.Infrastructure.MediatR.Clinics.Handlers;

public class AddClinicHandler : IRequestHandler<AddClinicCommand, Unit>
{
    private readonly IClinicRepository _clinicRepository;

    public AddClinicHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public async Task<Unit> Handle(AddClinicCommand request, CancellationToken cancellationToken)
    {
        var clinic = new Clinic()
        {
            Name = request.ClinicModel.Name,
            City = request.ClinicModel.City,
            PhoneNumber = request.ClinicModel.Phone,
            Address = request.ClinicModel.Address
        };

        await _clinicRepository.AddAsync(clinic);

        return Unit.Value;
    }
}

public class DeleteClinicByIdHandler : IRequestHandler<DeleteClinicByIdCommand, Unit>
{
    private readonly IClinicRepository _clinicRepository;

    public DeleteClinicByIdHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public async Task<Unit> Handle(DeleteClinicByIdCommand request, CancellationToken cancellationToken)
    {
        await _clinicRepository.DeleteAsync(request.Id);

        return Unit.Value;
    }
}

public class GetClinicByNameHandler : IRequestHandler<GetClinicByNameQuery, Clinic>
{
    private readonly IClinicRepository _clinicRepository;

    public GetClinicByNameHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public Task<Clinic> Handle(GetClinicByNameQuery request, CancellationToken cancellationToken) =>
        _clinicRepository.FindByNameAsync(request.Name);
}

public class GetClinicsByPageHandler : IRequestHandler<GetClinicsByPageQuery, IEnumerable<Clinic>>
{
    private readonly IClinicRepository _clinicRepository;

    public GetClinicsByPageHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public Task<IEnumerable<Clinic>> Handle(GetClinicsByPageQuery request, CancellationToken cancellationToken) =>
        _clinicRepository.FindByPageAsync(request.Page, request.PageSize);
}

public class GetClinicsHandler : IRequestHandler<GetClinicsQuery, IEnumerable<Clinic>>
{
    private readonly IClinicRepository _clinicRepository;

    public GetClinicsHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public Task<IEnumerable<Clinic>> Handle(GetClinicsQuery request, CancellationToken cancellationToken) =>
        _clinicRepository.FindAllAsync();
}

public class UpdateClinicHandler : IRequestHandler<UpdateClinicCommand, Unit>
{
    private readonly IClinicRepository _clinicRepository;

    public UpdateClinicHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public async Task<Unit> Handle(UpdateClinicCommand request, CancellationToken cancellationToken)
    {
        await _clinicRepository.UpdateAsync(request.Id, request.Clinic);

        return Unit.Value;
    }
}

public class SearchClinicsByNameHandler : IRequestHandler<SearchClinicsQuery, IEnumerable<Clinic>>
{
    private readonly IClinicRepository _clinicRepository;

    public SearchClinicsByNameHandler(IClinicRepository clinicRepository)
    {
        _clinicRepository = clinicRepository;
    }

    public Task<IEnumerable<Clinic>> Handle(SearchClinicsQuery request, CancellationToken cancellationToken) =>
        _clinicRepository.SearchAsync(request.Name);
}