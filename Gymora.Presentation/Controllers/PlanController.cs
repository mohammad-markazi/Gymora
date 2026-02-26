using Gymora.Database.Entities;
using Gymora.Service.Plan;
using Gymora.Service.Plan.Messaging;
using Gymora.Service.PlanTemplate;
using Gymora.Service.PlanTemplate.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlanController(IPlanService planService,IPlanTemplateService planTemplateService) : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAll(PlanStatus? status,string? fullName, CancellationToken cancellationToken)
        {
            var result = await planService.GetAllAsync(status,fullName, cancellationToken);
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

        [HttpPost("Finalize")]
        public async Task<IActionResult> FinalizePlan(IdRequest request, CancellationToken cancellationToken)
        {   
            var result = await planService.FinalizePlan(request, cancellationToken);
            return Ok(result);
        }

        #region Template
        [HttpPost("Template")]
        public async Task<IActionResult> CreateTemplate(CreateTemplateRequest request, CancellationToken cancellationToken)
        {
            var result = await planTemplateService.CreateAsync(request, cancellationToken);
            return Ok(result);
        }
        
        [HttpGet("Template")]
        public async Task<IActionResult> GetAllTemplate(CancellationToken cancellationToken)
        {
            var result = await planTemplateService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        
        [HttpDelete("Template/{id:int}")]
        public async Task<IActionResult> DeleteTemplate(int id, CancellationToken cancellationToken)
        {
            var result = await planTemplateService.DeleteAsync(id,cancellationToken);
            return Ok(result);
        }
        [HttpPut("Template")]
        public async Task<IActionResult> UpdateTemplate(EditTemplateRequest request, CancellationToken cancellationToken)
        {
            var result = await planTemplateService.UpdateAsync(request, cancellationToken);
            return Ok(result);
        }
        [HttpGet("Template/{id:int}")]
        public async Task<IActionResult> GetTemplateById([FromRoute]int id,CancellationToken cancellationToken)
        {
            var result = await planTemplateService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        
        [HttpPost("Template/Override")]
        public async Task<IActionResult> OverrideIntoPlan(OverrideIntoPlanRequest request, CancellationToken cancellationToken)
        {
            var result = await planTemplateService.OverrideIntoPlan(request, cancellationToken);
            return Ok(result);
        }

        #endregion
    }
}
