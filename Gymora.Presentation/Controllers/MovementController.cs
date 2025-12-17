using Gymora.Service.Movement;
using Gymora.Service.Movement.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController(IMovementService movementService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result =await movementService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateMovementRequest request,CancellationToken cancellationToken)
        {
            var result =await movementService.CreateAsync(request,cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await movementService.DeleteAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
