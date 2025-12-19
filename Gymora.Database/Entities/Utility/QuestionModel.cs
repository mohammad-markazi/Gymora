using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities.Utility
{
    public class QuestionModel
    {
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Body { get; set; }
        public int CreateCoachId { get; set; }
        [ForeignKey(nameof(CreateCoachId))]
        public CoachModel Coach { get; set; }
        public bool IsActive { get; set; }
    }
}
