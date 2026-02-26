using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities
{
    public class PlanTemplateModel
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Name { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        public int CreateCoachId { get; set; }
        public PlanStatus Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public CoachModel Coach { get; set; }
        public bool IsActive { get; set; }
        public int UsedCount { get; set; }
        public List<PlanTemplateDetailModel> TemplateDetails { get; set; }

    }
}
