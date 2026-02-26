using Gymora.Database.Entities;
using Gymora.Service.Plan.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.PlanTemplate.Messaging
{
    public class TemplateByIdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public PlanStatus Status { get; set; }
        public List<TemplateDetailViewModel> Details { get; set; } = new List<TemplateDetailViewModel>();

    }
}
