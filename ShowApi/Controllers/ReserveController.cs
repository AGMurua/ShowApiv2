using Microsoft.AspNetCore.Mvc;
using ShowApi.Managers;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReserveController : ControllerBase
    {
        private readonly ReserveManager _manager;

        public ReserveController(ReserveManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<ReserveDto>> GetAll()
        {
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RoomDTO> GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        //[HttpPut]
        //public IActionResult Create()
        //{
        //    var result = _manager.SaveRoom();
        //    return Created(Request.Path + "/" + result.Id, result);
        //}
        [HttpPatch("{id}")]
        public ActionResult Edit(string id)
        {
            return Ok(_manager.Update(id));
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok(_manager.Delete(id));
        }

    }
}
