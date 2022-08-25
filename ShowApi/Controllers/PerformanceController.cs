using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShowApi.Managers;
using ShowApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ShowApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PerformanceController : BaseController
    {
        private readonly PerformanceManager _manager;
        private readonly IMemoryCache _memoryCache;

        public PerformanceController(PerformanceManager manager, IMemoryCache memoryCache)
        {
            _manager = manager;
            _memoryCache = memoryCache;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpGet()]
        [AllowAnonymous]
        public ActionResult GetByFilter([FromQuery]decimal? minPrice, [FromQuery] decimal? maxPrice,
                                        [FromQuery] DateTime? minDate, [FromQuery] DateTime? maxDate,
                                        [FromQuery] IList<string> cast, [FromQuery] string genre)
        {
            return Ok(_manager.GetByFilter(minPrice, maxPrice, minDate, maxDate, cast, genre));
        }


        [HttpPost]
        public ActionResult Create(PerformanceCrudDTO dto, bool samePriceForAllSections = false, decimal? price = null)
        {
            if (!checkProfile())
                return Unauthorized();
            var result = _manager.SaveNewPerformance(dto, samePriceForAllSections, price);
            if (result.Code == "409")
                return Conflict(result);
            return Created(Request.Path + "/" + result.Data.Id, result.Data);
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
