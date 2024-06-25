//namespace Football.API.Controllers.v1
//{
//    using Football.Application.Dtos;
//    using Football.Domain.Contracts;
//    using Microsoft.AspNetCore.Http;
//    using Microsoft.AspNetCore.Mvc;
//    using System.Collections.Generic;
//    using System.Threading.Tasks;

//    [ApiController]
//    [ApiVersion("1.0")]
//    [Route("/api/v{version:apiVersion}/[controller]")]
//    public class StatisticsController : ControllerBase
//    {
//        private readonly IStatisticsService _statisticsService;

//        public StatisticsController(IStatisticsService statisticsService) => _statisticsService = statisticsService;

//        /// <summary>
//        /// Returns the entire list of of people who have participated in matches and have yellow cards.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <returns>List of statistics.</returns>
//        /// <response code="200">Ok.</response>
//        [HttpGet]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(List<StatisticDto>), StatusCodes.Status200OK)]
//        [Route("yellowcards")]
//        public async Task<IActionResult> GetYellowCardsAsync() => Ok(await _statisticsService.GetYellowCardsAsync());

//        /// <summary>
//        /// Returns the entire list of of people who have participated in matches and have red cards.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <returns>List of statistics.</returns>
//        /// <response code="200">Ok.</response>
//        [HttpGet]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(List<StatisticDto>), StatusCodes.Status200OK)]
//        [Route("redcards")]
//        public async Task<IActionResult> GetRedCardsAsync() => Ok(await _statisticsService.GetRedCardsAsync());

//        /// <summary>
//        /// Returns the entire list of of people who have participated in matches and have minutes played.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <returns>List of statistics.</returns>
//        /// <response code="200">Ok.</response>
//        [HttpGet]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(List<StatisticDto>), StatusCodes.Status200OK)]
//        [Route("minutesplayed")]
//        public async Task<IActionResult> GetMinutesPlayedAsync() => Ok(await _statisticsService.GetMinutesPlayedAsync());
//    }
//}
