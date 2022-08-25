using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using ShowApi.Managers;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RoomController : BaseController
    {
        private readonly RoomManager _manager;

        public RoomController(RoomManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<RoomDTO>> GetAll()
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RoomDTO> GetById(string id)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.GetById(id));
        }

        [HttpPut]
        public IActionResult Create([BindRequired]string name, string theatherID,IList<string> rooms)
        {
            if (!checkProfile())
                return Unauthorized();
            var result = _manager.SaveRoom(name, theatherID, rooms);
            return Created(Request.Path + "/" + result.Id, result);
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(string id, string name, IList<string> sections)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.Update(id, name, sections));
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.Delete(id));
        }

    }
}
