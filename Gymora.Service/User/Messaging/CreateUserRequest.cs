using Gymora.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.User.Messaging
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public UserType UserType { get; set; }
        public CoachModel Coach { get; set; }

    }
}
