using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Utilities
{
    public interface ILogService
    {
        Task<long> LogErrorAsync(Exception ex, string path, string requestBody);
    }
}
