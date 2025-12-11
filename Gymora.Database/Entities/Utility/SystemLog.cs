using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Database.Entities.Utility
{
    public class SystemLog
    {
        public int Id { get; set; }
        public long TrackingCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        [MaxLength(400)]
        public string Path { get; set; }
        public string RequestBody { get; set; }
        public DateTime Date { get; set; }
    }
}
