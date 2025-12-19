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
    public class MovementModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int? CreateCoachId { get; set; }
        [ForeignKey(nameof(CreateCoachId))]
        public CoachModel  Coach{ get; set; }
        public bool IsActive { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
