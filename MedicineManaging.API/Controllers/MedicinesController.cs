using MediatR;
using MedicineManaging.Domain.Entities.Medicines;
using MedicineManaging.Infrastructure.MediatR.Commands;
using MedicineManaging.Infrastructure.MediatR.Queries;
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
        public async Task<IActionResult> GetMedicineByIdAsync(string id)
        {
            var medicine = await _mediator.Send(new GetMedicineByIdQuery(id));

            return Ok(medicine);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicinesAsync()
        {
            var medicines = await _mediator.Send(new GetMedicinesQuery());

            return Ok(medicines);
        }

        [HttpPost]
        public async Task<IActionResult> AddMedicineAsync(Medicine medicine)
        {
            await _mediator.Send(new AddMedicineCommand(medicine));

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedicineAsync(string id, Medicine medicine)
        {
            await _mediator.Send(new UpdateMedicineCommand(id, medicine));

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedicineAsync(string id)
        {
            await _mediator.Send(new DeleteMedicineByIdCommand(id));

            return Ok();
        }
    }
}