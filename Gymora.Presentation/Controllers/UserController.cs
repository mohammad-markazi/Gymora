using Gymora.Service.User;
using Gymora.Service.User.Messaging;
using Gymora.Service.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IAuthService authService,IVerifyCodeService verifyCodeService) : ControllerBase
    {
        [HttpPost("VerifyCode")]
        public async Task<IActionResult> SendVerifyCode(SendVerifyCodeRequest request, CancellationToken cancellationToken)
        {
            await authService.SendVerifyCode(request.PhoneNumber, cancellationToken);
            return Ok(ResponseFactory.Success());
        }

        [HttpPost("VerifyCode/Confirm")]
        public async Task<IActionResult> ConfirmVerifyCode(ConfirmVerifyCodeRequest request,CancellationToken cancellationToken)
        {
            var checkCode = verifyCodeService.VerifyCode(new VerifyCodeViewModel()
                { PhoneNumber = request.PhoneNumber, Code = request.Code });
            return Ok(!checkCode ? ResponseFactory.Fail("کد وارد شده نامعتبر میباشد") : ResponseFactory.Success());
        }

    }
}
