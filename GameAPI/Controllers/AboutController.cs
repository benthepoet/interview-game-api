using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AboutController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Message = "Welcome to the Interview Game API!"
            });
        }
    }
}
