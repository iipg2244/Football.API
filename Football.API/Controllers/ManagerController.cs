using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        readonly FootballContext footballContext;
        public ManagerController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Manager>> Get()
        {
            return this.Ok(footballContext.Managers);
        }

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public ActionResult GetById(int id)
        {
            var response = footballContext.Managers.Find(id);
            if (response == default)
                this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public ActionResult Post(Manager manager)
        {
            var response = footballContext.Managers.Add(manager).Entity;
            return this.CreatedAtAction("GetById", response.Id, response);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Manager manager)
        {
            if (footballContext.Managers.Find(id) == default)
                return this.NotFound();

            footballContext.Managers.Update(manager);           
            return this.Ok();
        }
    }
}
