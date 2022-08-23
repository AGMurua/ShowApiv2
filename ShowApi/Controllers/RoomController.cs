using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using ShowApi.Managers;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        private readonly RoomManager _manager;

        public RoomController(RoomManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<RoomDTO>> GetAll()
        {
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<RoomDTO> GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpPut]
        public IActionResult SaveRoom()
        {
            return Ok();
        }

    }
}
