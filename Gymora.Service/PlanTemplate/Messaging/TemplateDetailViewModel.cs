using Gymora.Service.Plan.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.PlanTemplate.Messaging
{
    public class TemplateDetailViewModel
    {
        public int Id { get; set; }
        public byte Number { get; set; }
        public bool Complete { get; set; }
        public List<TemplateMovementViewModel> Movements { get; set; }
    }
}
