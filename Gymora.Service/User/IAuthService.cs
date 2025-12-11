using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User
{
    public interface IAuthService
    {
        Task SendVerifyCode(string phoneNumber, CancellationToken cancellation);

    }
}
