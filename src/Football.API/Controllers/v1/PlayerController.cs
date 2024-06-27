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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayerController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets list of players.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <returns>List of players.</returns>
        /// <response code="200">Ok.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PlayerFootball>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayersAsync() => Ok(await _playerService.GetPlayersAsync());

        /// <summary>
        /// Get player by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Player identifier.</param>
        /// <returns>If found, returns Player otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayerFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPlayerByIdAsync([FromHeader][Required] int id)
        {
            var response = await _playerService.GetPlayerByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<PlayerFootballExemple>(response));
        }

        /// <summary>
        /// Insert player.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="player">Player to insert.</param>
        /// <returns>If the insert is correct, returns Player otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPost("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayerFootball), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreatePlayerAsync([FromBody][Required] PlayerFootballExemple player)
        {
            var response = await _playerService.CreatePlayerAsync(_mapper.Map<PlayerFootball>(player));
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Update player by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Player identifier.</param>
        /// <param name="player">Player with the changes to be made.</param>
        /// <returns>If the update is correct, returns Player otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPut("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayerFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePlayerAsync([FromHeader][Required] int id, [FromBody][Required] PlayerFootballExemple player)
        {
            var response = await _playerService.UpdatePlayerAsync(id, _mapper.Map<PlayerFootball>(player));
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<PlayerFootballExemple>(response));
        }

        /// <summary>
        /// Delete player by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Player identifier.</param>
        /// <returns>If the delete is correct, returns true otherwise false.</returns>
        /// <response code="200">Ok.</response>
        [HttpDelete("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePlayerAsync([FromHeader][Required] int id) => Ok(await _playerService.DeletePlayerAsync(id));
    }
}
