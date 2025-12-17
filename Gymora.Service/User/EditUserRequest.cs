using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User
{
    public class EditUserRequest
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
