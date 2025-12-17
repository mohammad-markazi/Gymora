using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database.Entities;

namespace Gymora.Service.User
{
    public interface IAuthService
    {
        Task SendVerifyCode(string phoneNumber, CancellationToken cancellation);
        string GenerateToken(UserModel userModel);
        int GetCurrentUserId();
        string GetCurrentUserFullName();
        string GetCurrentUsername();
        int GetCurrentCoachId();
    }
}
