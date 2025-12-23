using Gymora.Service.VideoMovement;
using Gymora.Service.VideoMovement.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoMovementController(IVideoMovementService videoMovementService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await videoMovementService.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateVideoMovementRequest request, CancellationToken cancellationToken)
        {
            var result=await videoMovementService.CreateAsync(request, cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await videoMovementService.DeleteAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
