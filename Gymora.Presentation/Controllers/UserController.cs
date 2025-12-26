using Gymora.Database.Entities;
using Gymora.Service.User;
using Gymora.Service.User.Messaging;
using Gymora.Service.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IAuthService authService,IVerifyCodeService verifyCodeService,IUserService userService) : ControllerBase
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
            if (!checkCode) return Ok(ResponseFactory.Fail("کد وارد شده نامعتبر میباشد"));
           
            var user = userService.GetByUsername(request.PhoneNumber) ?? userService.Create(new CreateUserRequest()
            {
                PhoneNumber = request.PhoneNumber,
                UserType = UserType.Coach,
                Username = request.PhoneNumber,
                Coach = new CoachModel
                {
                    ProgramComplete = 0,
                    ProgramRequested = 0,
                    ProgramImperfect = 0,
                    ProgramSet = 0
                }
            }).Data;


            var token= authService.GenerateToken(user);
            return Ok(ResponseFactory.Success<string>(token));

        }

        [HttpPut]
        [Authorize]
        public IActionResult EditUser(EditUserRequest request)
        {
            request.UserId = authService.GetCurrentUserId();
            return Ok(userService.Edit(request));
        }


    }
}
