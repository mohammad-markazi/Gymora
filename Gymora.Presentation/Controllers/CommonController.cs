using Gymora.Service.Common;
using Gymora.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController(IFileUploader fileUploader) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromForm]UploadFileRequest request,CancellationToken cancellationToken)
        {
            var paths = new List<string>();
            foreach (var item in request.Files)
            {
                var result = await fileUploader.Upload(item, request.Paths.ToArray());
                paths.Add(result);
            }
            return Ok(ResponseFactory.Success<List<string>>(paths));
        }
    }
}
