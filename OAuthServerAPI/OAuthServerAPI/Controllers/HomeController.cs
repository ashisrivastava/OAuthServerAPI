using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
       
        return Ok(new { message = $"Hello, User! Welcome in authenticated API endpoint.", time = DateTime.UtcNow });
    }
}
