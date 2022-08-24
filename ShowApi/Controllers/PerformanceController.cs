using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowApi.Managers;
using ShowApi.Models;
using System;
using System.Security.Claims;

namespace ShowApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PerformanceController : BaseController
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
            if (!checkProfile())
                return Unauthorized();
                var result = _manager.SaveNewPerformance(dto, samePriceForAllSections, price);
            if (result.Code == "409")
                return Conflict(result);
            return Created(Request.Path + "/" + result.Data.Id, result.Data);
        }



        [HttpPatch("{id}")]
        public ActionResult Edit(string id, PerformanceCrudDTO dto)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.Update(id, dto));
        }
        [HttpDelete("{id}")]
        public ActionResult DeletePerformance(string id)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.Delete(id));
        }

    }
}
