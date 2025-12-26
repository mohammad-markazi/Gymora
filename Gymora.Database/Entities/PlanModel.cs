using Gymora.Database.Entities.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities
{
    public class PlanModel
    {
        public int Id { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(300)]
        public List<string> Files { get; set; }
        public byte Weight { get; set; }
        public byte Number { get; set; }
        public BitOperation<WeakMuscle, short> WeakMuscle { get; set; }
        public List<PlanQuestionModel> Questions { get; set; }
        public List<PlanDetailModel> PlanDetails { get; set; }
        public PlanStatus Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int CreateCoachId { get; set; }
    }

    public enum PlanStatus : byte
    {
        Unknown=0,
        Complete=1
    }

    public enum WeakMuscle : short
    {
        [Display(Name = "زیر بغل")]
        ZirBaghal=1,
        [Display(Name = "سرشانه")]
        Sarshane = 2,
        [Display(Name = "پشت بازو")]
        PoshtBazoo = 4,
        [Display(Name = "سینه")]
        Sine = 8,
        [Display(Name = "جلو بازو")]
        JeloBazoo = 16,
        [Display(Name = "پشت پا")]
        PoshtPa = 32,
        [Display(Name = "جلو پا")]
        JeloPa = 64,
        [Display(Name = "زیر سینه")]
        ZirSine = 128,
        [Display(Name = "بالا سینه")]
        BalaSine = 256
    }

}
