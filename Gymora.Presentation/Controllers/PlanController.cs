using Gymora.Database.Entities;
using Gymora.Service.Plan;
using Gymora.Service.Plan.Messaging;
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
        public async Task<IActionResult> Create(CreatePlanRequest request, CancellationToken cancellationToken)
        {
            var result = await planService.CreateAsync(request, cancellationToken);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(EditPlanRequest request, CancellationToken cancellationToken)
        {
            var result = await planService.UpdateAsync(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Movement")]
        public async Task<IActionResult> AddMovementToPlanDetail(PlanDetailMovementRequest request, CancellationToken cancellationToken)
        {
            var result = await planService.AddMovementToPlan(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("Detail/Finalize")]
        public async Task<IActionResult> FinalizePlanDetail(IdRequest request, CancellationToken cancellationToken)
        {
            var result = await planService.FinalizePlanDetail(request, cancellationToken);
            return Ok(result);
        }
        [HttpPost("Finalize")]
        public async Task<IActionResult> FinalizePlan(IdRequest request, CancellationToken cancellationToken)
        {   
            var result = await planService.FinalizePlan(request, cancellationToken);
            return Ok(result);
        }
    }
}
