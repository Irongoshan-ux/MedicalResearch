using MediatR;
using MedicineManaging.Domain.Entities.Clinics;

namespace MedicineManaging.Infrastructure.MediatR.Clinics.Commands;

public record AddClinicCommand(AddClinicModel ClinicModel) : IRequest;
public record DeleteClinicByIdCommand(string Id) : IRequest;
public record UpdateClinicCommand(string Id, Clinic Clinic) : IRequest;