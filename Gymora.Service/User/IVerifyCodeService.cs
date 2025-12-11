using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Service.User.Messaging;

namespace Gymora.Service.User
{
    public interface IVerifyCodeService
    {
        bool CheckExistsSendVerifyCode(string phoneNumber);
        bool VerifyCode(VerifyCodeViewModel model);
        long? CheckExistsUserForSendVerifyCode(string phoneNumber, UserType userType);
        Task<string> SendVerifyCode(string phoneNumber);
    }
}
