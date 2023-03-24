using Microsoft.AspNetCore.Mvc;
using System;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        readonly FootballContext footballContext;
        public StatisticsController(FootballContext footballContext)
        {
            this.footballContext = footballContext;
        }

        [HttpGet]
        [Route("yellowcards")]
        public ActionResult GetYellowCards()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("redcards")]
        public ActionResult GetRedCards()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("minutesplayed")]
        public ActionResult GetMinutesPlayed()
        {
            throw new NotImplementedException();
        }
    }
}
