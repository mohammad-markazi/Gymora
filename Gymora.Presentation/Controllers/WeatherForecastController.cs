using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Gymora.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment=webHostEnvironment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            throw new NotImplementedException();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost]
        [Route("file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadRequest file)
        {
            if (file.File == null || file.File.Length == 0)
                return BadRequest("هیچ فایلی ارسال نشده است.");

            // مسیر پوشه‌ی ذخیره سازی
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // تولید نام یکتا برای فایل
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.File.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            // ذخیره فایل
            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.File.CopyToAsync(stream);
            }

            // تولید URL برای دسترسی به فایل
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            return Ok(new { url = fileUrl });
        }
    }
    public class UploadRequest
    {
        public IFormFile File { get; set; }
    }
}
