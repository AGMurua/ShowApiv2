﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPut]
        public IActionResult Create(TicketDto ticket)
        {
            var result = _manager.SaveTicket(ticket);
            return Created(Request.Path + "/" + result.Id, result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok(_manager.Delete(id));
        }

    }
}
