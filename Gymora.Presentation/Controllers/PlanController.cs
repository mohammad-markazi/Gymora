using Gymora.Database.Entities;
using Gymora.Service.Plan;
using Gymora.Service.Plan.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController(IPlanService planService) : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll(PlanStatus? status, CancellationToken cancellationToken)
        {
            var result = await planService.GetAllAsync(status, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await planService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePlanRequest request, CancellationToken cancellationToken)
        {
            var result = await planService.CreateAsync(request, cancellationToken);
            return Ok(result);
        }
    }
}
