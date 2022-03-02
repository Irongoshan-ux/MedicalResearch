using MediatR;
using MedicineManaging.API.Utilities.Attributes;
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
        [Access(Roles = new[] { "admin", "user" })]
        public async Task<IActionResult> GetClinicsAsync()
        {
            var clinics = await _mediator.Send(new GetClinicsQuery());

            return GetResult(clinics);
        }

        [HttpGet]
        [Route("FindClinicsByPage")]
        [Access(Roles = new[] { "admin", "user" })]
        public async Task<IActionResult> GetClinicsByPageAsync(int page = 0, int pageSize = 5)
        {
            var clinics = await _mediator.Send(new GetClinicsByPageQuery(page, pageSize));

            _logger.LogInformation($"Returned '{clinics.Count()}' clinics");

            return GetResult(clinics);
        }

        [HttpPost]
        [Route("Create")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> AddClinicAsync(AddClinicModel clinic)
        {
            await _mediator.Send(new AddClinicCommand(clinic));

            _logger.LogInformation($"Added '{clinic.Name}' clinic");

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> UpdateClinicAsync(string id, Clinic clinic)
        {
            await _mediator.Send(new UpdateClinicCommand(id, clinic));

            _logger.LogInformation($"Updated '{clinic.Id}' clinic");

            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        [Access(Roles = new[] { "admin" })]
        public async Task<IActionResult> DeleteClinicAsync(string id)
        {
            await _mediator.Send(new DeleteClinicByIdCommand(id));

            _logger.LogInformation($"Deleted '{id}' clinic");

            return Ok();
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> SearchClinicsAsync(string name)
        {
            var clinics = await _mediator.Send(new SearchClinicsQuery(name));

            _logger.LogInformation($"Found '{clinics.Count()}' clinics");

            return GetResult(clinics);
        }

        private IActionResult GetResult(IEnumerable<Clinic> clinics) =>
            clinics.Any() ? Ok(clinics) : NotFound();
    }
}
