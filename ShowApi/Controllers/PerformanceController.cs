using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowApi.Managers;
using ShowApi.Models;
using System;

namespace ShowApi.Controllers
{
    [ApiController]
    [Authorize]
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
        public ActionResult Create(PerformanceCrudDTO dto, bool samePriceForAllSections = false, decimal? price = null)
        {
            var result = _manager.SaveNewPerformance(dto, samePriceForAllSections, price);
            if (result.Code == "409")
                return Conflict(result);
            return Created(Request.Path + "/" + result.Data.Id, result.Data);
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(string id)
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
