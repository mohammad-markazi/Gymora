using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User.Messaging
{
    public class SmsVerifyCodeRequestDto
    {
        public string Mobile { get; set; }

        public int TemplateId { get; set; }
        public List<ParameterObject> Parameters { get; set; }
    }
    public class ParameterObject
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
