using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymora.Service.Utilities
{
    public static class DateTimeExtension
    {
        public static string ToRelativeTime(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime.ToUniversalTime();

            if (timeSpan.TotalSeconds < 60)
                return "چند ثانیه پیش";

            if (timeSpan.TotalMinutes < 60)
                return $"{Math.Floor(timeSpan.TotalMinutes)} دقیقه پیش";

            if (timeSpan.TotalHours < 24)
                return $"{Math.Floor(timeSpan.TotalHours)} ساعت پیش";

            if (timeSpan.TotalDays < 7)
                return $"{Math.Floor(timeSpan.TotalDays)} روز پیش";

            if (timeSpan.TotalDays < 30)
                return $"{Math.Floor(timeSpan.TotalDays / 7)} هفته پیش";

            if (timeSpan.TotalDays < 365)
                return $"{Math.Floor(timeSpan.TotalDays / 30)} ماه پیش";

            return $"{Math.Floor(timeSpan.TotalDays / 365)} سال پیش";
        }

    }
}
