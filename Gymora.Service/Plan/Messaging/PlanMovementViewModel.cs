using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Service.Movement.Messaging;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanMovementViewModel
    {
        public int Id { get; set; }
        public int MovementId { get; set; }
        public string MovementName { get; set; }

        public int Code { get; set; }
        public int OrderBy { get; set; }
        public string Pattern { get; set; }
        public string Description { get; set; }
        public bool Parent { get; set; }
 
    }
}
