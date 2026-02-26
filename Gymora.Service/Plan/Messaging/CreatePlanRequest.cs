using System.ComponentModel.DataAnnotations;
using Gymora.Database.Entities;
using Gymora.Database.Entities.Utility;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Gymora.Service.Plan.Messaging
{
    public class CreatePlanRequest
    {
        [Required(ErrorMessage = "شماره موبایل اجباری میباشد")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "شماره موبایل اجباری میباشد")]
        public string FullName { get; set; }
        public List<string>? Files { get; set; } 
        public byte? Weight { get; set; }
        public byte? Number { get; set; }
        public short? WeakMuscle { get; set; }
        public List<PlanQuestionRequest>? Questions { get; set; }

    }
}
