using MediatR;
using MedicineManaging.Domain.Entities.Patients;

namespace MedicineManaging.Infrastructure.MediatR.Patients.Commands;

public record AddPatientCommand(Patient Patient) : IRequest;
public record DeletePatientByIdCommand(int Id) : IRequest;
public record UpdatePatientCommand(int Id, Patient Patient) : IRequest;