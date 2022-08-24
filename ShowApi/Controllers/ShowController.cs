using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("[controller]")]
    public class ShowController : BaseController
    {
        private readonly ShowManager _manager;

        public ShowController(ShowManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<ShowDTO>> GetAll()
        {
            return Ok(_manager.GetAll());        
        }

        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpPut]
        public ActionResult Create(CrudShowDTO show)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.SaveNewShow(show));
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(string id, CrudShowDTO dto)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.Update(dto,id));
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
