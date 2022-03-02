using MediatR;
using MedicineManaging.API.Utilities.Attributes;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Infrastructure.MediatR.Medicines.Commands;
using MedicineManaging.Infrastructure.MediatR.Medicines.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManaging.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly ILogger<MedicinesController> _logger;
        private readonly IMediator _mediator;

        public MedicinesController(ILogger<MedicinesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("FindById")]
        public async Task<IActionResult> GetMedicineByIdAsync(string id)
        {
            var medicine = await _mediator.Send(new GetMedicineByIdQuery(id));

            _logger.LogInformation($"Returned '{medicine.Id}' medicine");

            if (medicine is null) return NotFound();

            return Ok(medicine);
        }

        [HttpGet]
        [Route("FindAll")]
        public async Task<IActionResult> GetMedicinesAsync()
        {
            var medicines = await _mediator.Send(new GetMedicinesQuery());

            _logger.LogInformation($"Returned '{medicines.Count()}' medicines");

            if (medicines.Any()) return Ok(medicines);

            return NotFound();
        }

        [HttpGet]
        [Route("FindMedicinesByPage")]
        public async Task<IActionResult> GetMedicinesByPageAsync(int page = 0, int pageSize = 5)
        {
            var medicines = await _mediator.Send(new GetMedicinesByPageQuery(page, pageSize));

            _logger.LogInformation($"Returned '{medicines.Count()}' medicines");

            if (medicines.Any()) return Ok(medicines);

            return NotFound();
        }

        [HttpPost]
        [Route("Create")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> AddMedicineAsync(Medicine medicine)
        {
            await _mediator.Send(new AddMedicineCommand(medicine));

            _logger.LogInformation($"Added '{medicine.Id}' medicine");

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> UpdateMedicineAsync(string id, Medicine medicine)
        {
            await _mediator.Send(new UpdateMedicineCommand(id, medicine));

            _logger.LogInformation($"Updated '{medicine.Id}' medicine");

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> DeleteMedicineAsync(string id)
        {
            await _mediator.Send(new DeleteMedicineByIdCommand(id));

            _logger.LogInformation($"Deleted '{id}' medicine");

            return Ok();
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchMedicineAsync(MedicineType? medicineType, Container? container)
        {
            var medicines = await _mediator.Send(new SearchMedicinesQuery(medicineType, container));

            _logger.LogInformation($"Returned '{medicines.Count()}' medicines by searching");

            if (medicines.Any()) return Ok(medicines);

            return NotFound();
        }
    }
}