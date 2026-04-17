using Microsoft.AspNetCore.Mvc;

namespace QuantityMeasurementWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Service = "QuantityMeasurement API",
                Version = "1.0.0"
            });
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Get();
        }
    }
}
