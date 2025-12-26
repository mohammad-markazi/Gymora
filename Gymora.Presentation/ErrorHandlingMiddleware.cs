using System.Text;
using Gymora.Service.Utilities;

namespace Gymora.Presentation
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogService logService)
        {
            string requestBody = "";
            try
            {
                context.Request.EnableBuffering();

                // خواندن Request Body فقط یک‌بار
                if (context.Request.ContentLength > 0)
                {
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    requestBody = await reader.ReadToEndAsync();

                    // ریست کردن تا کنترلر بتواند دوباره بخواند
                    context.Request.Body.Position = 0;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                // خواندن مجدد request body (بعد از خطا)
                context.Request.Body.Position = 0;

                var bodyString = "";
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    bodyString = await reader.ReadToEndAsync();
                }

                // ثبت لاگ با Body
                var tracking = await logService.LogErrorAsync(
                    ex,
                    context.Request.Path,
                    bodyString
                );

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    success=false,
                    message = $"شماره رهگیری:{tracking} خطایی در سرور رخ داده"
                }));
            }
        }
    }
}
