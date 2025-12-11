using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User.Messaging
{
    public class ConfirmVerifyCodeRequest:SendVerifyCodeRequest
    {
        public string Code { get; set; }
    }
}
