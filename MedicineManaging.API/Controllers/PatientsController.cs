using MediatR;
using MedicineManaging.API.Utilities.Attributes;
using MedicineManaging.Domain.Entities.Patients;
using MedicineManaging.Infrastructure.MediatR.Patients.Commands;
using MedicineManaging.Infrastructure.MediatR.Patients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManaging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ILogger<PatientsController> _logger;
        private readonly IMediator _mediator;

        public PatientsController(ILogger<PatientsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("FindAll")]
        [Access(Roles = new[] { "admin", "user" })]
        public async Task<IActionResult> GetPatientsAsync()
        {
            var clinics = await _mediator.Send(new GetPatientsQuery());

            if (clinics is null) return NotFound();

            return Ok(clinics);
        }

        [HttpGet]
        [Route("FindClinicsByPage")]
        [Access(Roles = new[] { "admin", "user" })]
        public async Task<IActionResult> GetPatientsByPageAsync(int page = 0, int pageSize = 5)
        {
            var clinics = await _mediator.Send(new GetPatientsByPageQuery(page, pageSize));

            if (clinics is null) return NotFound();

            return Ok(clinics);
        }

        [HttpPost]
        [Route("Register")]
        [Access(Roles = new[] { "admin", "user" })]
        public async Task<IActionResult> RegisterPatientAsync(RegisterPatientModel patient)
        {
            await _mediator.Send(new RegisterPatientCommand(patient));

            return Ok();
        }

        [HttpPost]
        [Route("Create")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> AddPatientAsync(Patient patient)
        {
            await _mediator.Send(new AddPatientCommand(patient));

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> UpdatePatientAsync(int id, Patient patient)
        {
            await _mediator.Send(new UpdatePatientCommand(id, patient));

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> DeletePatientAsync(int id)
        {
            await _mediator.Send(new DeletePatientByIdCommand(id));

            return Ok();
        }
    }
}
