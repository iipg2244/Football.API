using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService= statisticsService;
        }

        [HttpGet]
        [Route("yellowcards")]
        public async Task<IActionResult> GetYellowCardsAsync()
        {
            return Ok(await _statisticsService.GetYellowCardsAsync());
        }

        [HttpGet]
        [Route("redcards")]
        public async Task<IActionResult> GetRedCardsAsync()
        {
            return Ok(await _statisticsService.GetRedCardsAsync());
        }

        [HttpGet]
        [Route("minutesplayed")]
        public async Task<IActionResult> GetMinutesPlayedAsync()
        {
            return Ok(await _statisticsService.GetMinutesPlayedAsync());
        }
    }
}
