using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities
{
    public class CoachModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProgramComplete { get; set; } // برنامه های تمام شده
        public int ProgramRequested { get; set; } // برنامه های درخواستی
        public int ProgramImperfect { get; set; } // برنامه های ناقص
        public int ProgramSet { get; set; } // برنامه های ارسال شده
        public UserModel User { get; set; }
    }
}
