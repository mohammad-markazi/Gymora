using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database.Entities;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }

        public string FullName { get; set; }
        public byte Weight { get; set; }
        public string CreateDate { get; set; }
        public PlanStatus Status { get; set; }

    }
}
