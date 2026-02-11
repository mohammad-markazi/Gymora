using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Gymora.Service.Common
{
    public class UploadFileRequest
    {
        public List<IFormFile> Files { get; set; }
        public List<string>  Paths{ get; set; }
    }
}
