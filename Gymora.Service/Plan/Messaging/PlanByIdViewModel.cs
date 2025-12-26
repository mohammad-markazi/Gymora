using Gymora.Database.Entities;
using Gymora.Database.Entities.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Plan.Messaging
{
    public class PlanByIdViewModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public List<string> Files { get; set; }
        public byte Weight { get; set; }
        public byte Number { get; set; }
        public IEnumerable<short> WeakMuscle { get; set; }=new List<short>();
        public List<PlanQuestionViewModel> Questions { get; set; }= new List<PlanQuestionViewModel>();
        public List<PlanDetailViewModel> Details { get; set; } = new List<PlanDetailViewModel>();
    }
}
