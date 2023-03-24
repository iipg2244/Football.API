using Football.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly FootballContext footballContext;
        public MatchController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<IEnumerable<Match>> Get()
        {
            return this.Ok(footballContext.Matches);
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public ActionResult GetById(int id)
        {
            var response = footballContext.Matches.Find(id);
            if (response == default)
                this.NotFound();
            return this.Ok();
        }

        [HttpPost]
        public ActionResult Post(Match match)
        {
            var response = footballContext.Matches.Add(match).Entity;
            return this.CreatedAtAction("GetById", response.Id, response);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Match match)
        {
            if (footballContext.Matches.Find(id) == default)
                return this.NotFound();

            footballContext.Matches.Update(match);
            return this.Ok();
        }
    }
}
