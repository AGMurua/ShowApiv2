using Microsoft.AspNetCore.Mvc;
using ShowApi.Managers;
using ShowApi.Models;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly PerformanceManager _manager;

        public PerformanceController(PerformanceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_manager.GetAll());
        }

        [HttpPut]
        public ActionResult SaveNewPerformance(PerformanceCrudDTO dto)
        {
            return Ok(_manager.SaveNewPerformance(dto));
        }
        [HttpPatch]
        public ActionResult EditPerformance()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePerformance(string id)
        {
            return Ok(_manager.Delete(id));
        }

    }
}
