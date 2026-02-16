using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanDetailViewModel
    {
        public int Id { get; set; }
        public byte Number { get; set; }
        public bool Complete { get; set; }
        public List<PlanMovementViewModel> Movements { get; set; }

    }
}
