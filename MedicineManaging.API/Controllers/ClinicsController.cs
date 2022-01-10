using MediatR;
using MedicineManaging.Domain.Entities.Clinics;
using MedicineManaging.Infrastructure.MediatR.Clinics.Commands;
using MedicineManaging.Infrastructure.MediatR.Clinics.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly ILogger<ClinicsController> _logger;
        private readonly IMediator _mediator;

        public ClinicsController(ILogger<ClinicsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("FindAll")]
        public async Task<IActionResult> GetClinicsAsync()
        {
            var clinics = await _mediator.Send(new GetClinicsQuery());

            if (clinics is null) return NotFound();

            return Ok(clinics);
        }

        [HttpGet]
        [Route("FindClinicsByPage")]
        public async Task<IActionResult> GetClinicsByPageAsync(int page = 0, int pageSize = 5)
        {
            var clinics = await _mediator.Send(new GetClinicsByPageQuery(page, pageSize));

            if (clinics is null) return NotFound();

            return Ok(clinics);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddClinicAsync(Clinic clinic)
        {
            await _mediator.Send(new AddClinicCommand(clinic));

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClinicAsync(string id, Clinic clinic)
        {
            await _mediator.Send(new UpdateClinicCommand(id, clinic));

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteClinicAsync(string id)
        {
            await _mediator.Send(new DeleteClinicByIdCommand(id));

            return Ok();
        }
    }
}
