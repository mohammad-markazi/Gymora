using Gymora.Database.Entities;
using Gymora.Database.Entities.Utility;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Plan.Messaging
{
    public class CreatePlanRequest
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public List<IFormFile>? Files { get; set; }
        public byte Weight { get; set; }
        public byte Number { get; set; }
        public BitOperation<WeakMuscle, short> WeakMuscle { get; set; } = new();
        public List<PlanQuestionRequest> Questions { get; set; }=new List<PlanQuestionRequest>();

    }
}
