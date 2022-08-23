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
    [Route("[controller]")]
    public class SectionController : Controller
    {
        private readonly SectionManager _manager;

        public SectionController(SectionManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<SectionDTO>> GetAll()
        {
            return Ok(_manager.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<SectionDTO> GetById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpPut]
        public IActionResult SaveSection([BindRequired]string name, [BindRequired]IList<string> seat)
        {
            var result = _manager.SaveSection(name, seat);
            return Created(Request.Path + "/" + result.Id, result);
        }

    }
}
