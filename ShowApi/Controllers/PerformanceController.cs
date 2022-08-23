using Microsoft.AspNetCore.Mvc;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult GetById()
        {
            return Ok();
        }
    }
}
