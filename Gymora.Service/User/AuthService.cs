using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;
using Gymora.Database.Entities;

namespace Gymora.Service.User
{
    public class AuthService(IVerifyCodeService verifyCodeService,IGymoraDbContext context):IAuthService
    {
        public async Task SendVerifyCode(string phoneNumber, CancellationToken cancellationToken)
        {
            if (!verifyCodeService.CheckExistsSendVerifyCode(phoneNumber))
            {
                var code = await verifyCodeService.SendVerifyCode(phoneNumber);
                await context.VerifyCodes.AddAsync(new VerifyCodeModel()
                {
                    PhoneNumber = phoneNumber,
                    Code = code,
                    IsActive = true,
                }, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
