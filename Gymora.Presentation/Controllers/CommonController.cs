using Gymora.Service.Common;
using Gymora.Service.Utilities;
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
          var result=await  fileUploader.Upload(request.File, request.Paths.ToArray());
          return Ok(ResponseFactory.Success<string>(result));
        }
    }
}
