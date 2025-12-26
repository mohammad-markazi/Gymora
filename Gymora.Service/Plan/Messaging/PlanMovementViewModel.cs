using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanMovementViewModel
    {
        public int Id { get; set; }
        public int MovementId { get; set; }
        public MovementModel Movement { get; set; }
        public byte Set { get; set; }
        public byte Count { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
    }
}
