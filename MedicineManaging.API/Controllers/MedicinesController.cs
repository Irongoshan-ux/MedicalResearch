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
        [Route("FindById")]
        public async Task<IActionResult> GetMedicineByIdAsync(string id)
        {
            var medicine = await _mediator.Send(new GetMedicineByIdQuery(id));

            if (medicine is null) return NotFound();

            return Ok(medicine);
        }

        [HttpGet]
        [Route("FindAll")]
        public async Task<IActionResult> GetMedicinesAsync()
        {
            var medicines = await _mediator.Send(new GetMedicinesQuery());

            if(medicines is null) return NotFound();

            return Ok(medicines);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddMedicineAsync(Medicine medicine)
        {
            await _mediator.Send(new AddMedicineCommand(medicine));

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateMedicineAsync(string id, Medicine medicine)
        {
            await _mediator.Send(new UpdateMedicineCommand(id, medicine));

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteMedicineAsync(string id)
        {
            await _mediator.Send(new DeleteMedicineByIdCommand(id));

            return Ok();
        }
    }
}