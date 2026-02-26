using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.PlanTemplate.Messaging
{
    public class CreateTemplateRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
    public class OverrideIntoPlanRequest
    {
       public int PlanId { get; set; }
       public int TemplateId { get; set; }

    }

    public class EditTemplateRequest : CreateTemplateRequest
    {
        public int Id { get; set; }
    }
}
