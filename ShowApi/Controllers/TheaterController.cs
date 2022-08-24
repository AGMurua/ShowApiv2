using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Driver;
using ShowApi.Data.Entities;
using ShowApi.Managers;
using ShowApi.Models;
using System;
using System.Collections.Generic;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheaterController : Controller
    {
        private readonly TheaterManager _manager;

        public TheaterController(TheaterManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IList<TheaterDTO>> GetAll()
        {
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TheaterDTO> GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpPut]
        public ActionResult Create(TheaterCrudDTO dto)
        {
            var result = _manager.SaveTheater(dto);
            return Created(Request.Path + "/" + result.Id, result);
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(string id, TheaterCrudDTO dto)
        {
            return Ok(_manager.Update(id, dto));
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok(_manager.Delete(id));
        }

    }
}
