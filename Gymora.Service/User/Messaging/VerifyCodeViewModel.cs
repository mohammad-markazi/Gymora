using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User.Messaging
{
    public class VerifyCodeViewModel
    {
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
    }
}
