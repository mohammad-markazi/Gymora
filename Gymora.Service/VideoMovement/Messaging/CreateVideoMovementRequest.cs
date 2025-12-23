using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.VideoMovement.Messaging
{
    public class CreateVideoMovementRequest
    {
        public IFormFile File { get; set; }
        public int  MovementId { get; set; }
    }

}
