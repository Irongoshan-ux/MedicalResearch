using MediatR;
using MedicineManaging.Domain.Entities.Medicines;

namespace MedicineManaging.Infrastructure.MediatR.Medicines.Commands;

public record AddMedicineCommand(Medicine Medicine) : IRequest;
public record DeleteMedicineByIdCommand(string Id) : IRequest;
public record UpdateMedicineCommand(string Id, Medicine Medicine) : IRequest;