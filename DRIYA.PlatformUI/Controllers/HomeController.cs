using Microsoft.AspNetCore.Mvc;

namespace DRIYA.PlatformUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "DRIYA Platform API is running!", timestamp = DateTime.UtcNow });
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        return Ok(new { 
            status = "online", 
            version = "1.0.0", 
            timestamp = DateTime.UtcNow,
            services = new[] { "Frontend", "API", "Database" }
        });
    }
}
