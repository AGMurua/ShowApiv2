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
    public class SectionController : BaseController
    {
        private readonly SectionManager _manager;

        public SectionController(SectionManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<SectionDTO>> GetAll()
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<SectionDTO> GetById(string id)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.GetById(id));
        }

        [HttpPost]
        public ActionResult Create([BindRequired]string name, [BindRequired]int numberOfSeat, string roomdId)
        {
            if (!checkProfile())
                return Unauthorized();
            var result = _manager.SaveSection(name, numberOfSeat, roomdId);
            return Created(Request.Path + "/" + result.Id, result);
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(string id, string name, int numberOfSeat)
        {
            if (!checkProfile())
                return Unauthorized();
            return Ok(_manager.UpdateSection(name, numberOfSeat,id));
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
