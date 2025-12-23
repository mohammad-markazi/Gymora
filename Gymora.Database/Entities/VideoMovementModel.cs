using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities
{
    public class VideoMovementModel
    {
        public int Id { get; set; }
        public int MovementId { get; set; }
        public MovementModel Movement { get; set; }
        [MaxLength(500)]
        public string Link { get; set; }
        public int CreateCoachId { get; set; }
        [ForeignKey(nameof(CreateCoachId))]
        public CoachModel Coach { get; set; }
        public bool IsActive { get; set; }
    }
}
