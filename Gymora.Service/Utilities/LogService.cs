using Gymora.Database.Entities.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database;

namespace Gymora.Service.Utilities
{
    public class LogService(IGymoraDbContext context):ILogService
    {
        public async Task<long> LogErrorAsync(Exception ex, string path, string requestBody)
        {
            var trackingCode = await GenerateUniqueTrackingCodeAsync();

            var log = new SystemLog
            {
                TrackingCode = trackingCode,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Path = path,
                RequestBody = requestBody,
                Date = DateTime.UtcNow
            };

            await context.SystemLogs.AddAsync(log);
            await context.SaveChangesAsync(CancellationToken.None);

            return trackingCode;
        }
        private async Task<long> GenerateUniqueTrackingCodeAsync()
        {
            long code;
            var rand = new Random();

            do
            {
                code = rand.Next(100000, 999999);
            }
            while (await context.SystemLogs.AnyAsync(x => x.TrackingCode == code));

            return code;
        }
    }
}
