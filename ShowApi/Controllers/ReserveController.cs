using Microsoft.AspNetCore.Mvc;
using ShowApi.Managers;
using ShowApi.Models;
using System.Collections.Generic;

namespace ShowApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReserveController : BaseController
    {
        private readonly ReserveManager _manager;

        public ReserveController(ReserveManager manager)
        {
            _manager = manager;
        }


        [HttpGet]
        public ActionResult<IList<ReserveDto>> GetAll()
        {
            return Ok(_manager.GetAll(userId(),profile()));
        }

        [HttpPut]
        public ActionResult Create(ReserveCrudDto ticket)
        {
            var result = _manager.SaveTicket(ticket, userId(), userName());
            if (result.Code == "409")
                return Conflict(result);
            return Created(Request.Path + "/" + result.Data.Id, result);
        }

    }
}
