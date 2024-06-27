namespace Football.API.Controllers.v1
{
    using Football.Domain.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Exemples;
    using Football.Domain.Entities.Football;
    using AutoMapper;
    using Football.Application.Services;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMapper _mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            _matchService = matchService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets list of matchs.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <returns>List of matchs.</returns>
        /// <response code="200">Ok.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MatchFootball>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMatchesAsync() => Ok(await _matchService.GetMatchesAsync());

        /// <summary>
        /// Get match by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Match identifier.</param>
        /// <returns>If found, returns Match otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MatchFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMatchByIdAsync([FromHeader][Required] int id)
        {
            var response = await _matchService.GetMatchByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<MatchFootballExemple>(response));
        }

        /// <summary>
        /// Insert match.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="match">Match to insert.</param>
        /// <returns>If the insert is correct, returns Match otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPost("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MatchFootball), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMatchAsync([FromBody][Required] MatchFootballExemple match)
        {
            var response = await _matchService.CreateMatchAsync(_mapper.Map<MatchFootball>(match));
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Update match by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Match identifier.</param>
        /// <param name="match">Match with the changes to be made.</param>
        /// <returns>If the update is correct, returns Match otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPut("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MatchFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMatchAsync([FromHeader][Required] int id, [FromBody][Required] MatchFootballExemple match)
        {
            var response = await _matchService.UpdateMatchAsync(id, _mapper.Map<MatchFootball>(match));
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<MatchFootballExemple>(response));
        }

        /// <summary>
        /// Delete match by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Match identifier.</param>
        /// <returns>If the delete is correct, returns true otherwise false.</returns>
        /// <response code="200">Ok.</response>
        [HttpDelete("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteMatchAsync([FromHeader][Required] int id) => Ok(await _matchService.DeleteMatchAsync(id));
    }
}
