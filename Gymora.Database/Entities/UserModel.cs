using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities
{
    public class UserModel
    {
        public int Id { get; set; }
        [MaxLength(70)]
        public string Username { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        [MaxLength(200)]
        public string? FullName { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? LastLoginDate { get; set; }
        public CoachModel Coach { get; set; }


    }
    public enum UserType
    {
        Coach = 1,
        BodyBuilder = 2
    }
}
