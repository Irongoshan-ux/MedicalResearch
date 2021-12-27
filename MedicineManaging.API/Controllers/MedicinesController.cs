using MediatR;
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
    }
}