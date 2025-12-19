using Gymora.Service.Question;
using Gymora.Service.Question.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController(IQuestionService questionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await questionService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionRequest request,CancellationToken cancellationToken)
        {
            var result = await questionService.CreateAsync(request,cancellationToken);
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            var result = await questionService.DeleteAsync(id, cancellationToken);
            return Ok(result);
        }
    }
}
