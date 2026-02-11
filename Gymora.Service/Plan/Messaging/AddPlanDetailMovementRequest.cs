using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanDetailMovementRequest
    {
        public int PlanDetailId { get; set; }
        public List<AddPlanDetailMovementRequest> Movements { get; set; }
    }
    public class AddPlanDetailMovementRequest
    {
        public int MovementId { get; set; }
        public int Code { get; set; }
        public int OrderBy { get; set; }
        public string Pattern { get; set; }
        public string Description { get; set; }
        public bool Parent { get; set; }
    }
}
