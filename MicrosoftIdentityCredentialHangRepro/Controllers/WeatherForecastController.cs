using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;

namespace MicrosoftIdentityCredentialHangRepro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IHttpContextAccessor httpContextAccessor, ILogger<WeatherForecastController> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is not null)
            {
                _logger.LogInformation("start get MicrosoftIdentityTokenCredential");
                var tokenCredential = httpContext.RequestServices.GetRequiredService<MicrosoftIdentityTokenCredential>();
                _logger.LogInformation("end getting MicrosoftIdentityTokenCredential");
            }
            else
            {
                _logger.LogError("No http context");
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }
    }
}
