using System.ComponentModel.DataAnnotations;

namespace Gymora.Database.Entities
{
    public class VerifyCodeModel
    {
        public int Id { get; set; }
        [MaxLength(5)]
        public string Code { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
