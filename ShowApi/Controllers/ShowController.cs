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
    public class ShowController : Controller
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
        public ActionResult GetDetailById(string id)
        {
            return Ok(_manager.GetById(id));
        }

        [HttpPost]
        public ActionResult NewShow(ShowDTO show)
        {
            return Ok(_manager.SaveNewShow(show));
        }
    }
}
