using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        readonly FootballContext footballContext;
        public PlayerController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Player>> Get()
        {
            return this.Ok(footballContext.Players);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var response = footballContext.Players.Find(id);
            if (response == default)
                this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public ActionResult Post(Player player)
        {
            var response = footballContext.Players.Add(player).Entity;
            return this.CreatedAtAction("GetById", response.Id, response);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Player player)
        {
            if (footballContext.Players.Find(id) == default)
                return this.NotFound();

            footballContext.Players.Update(player);        
            return this.Ok();
        }
    }
}
