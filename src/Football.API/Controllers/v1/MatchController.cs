namespace Football.API.Controllers.v1
{
    using Football.Domain.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Exemples;
    using Football.Domain.Entities.Football;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService) => _matchService = matchService;

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
        [ProducesResponseType(typeof(List<MatchExemple>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync() => Ok(await _matchService.GetAsync());

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
        [ProducesResponseType(typeof(MatchExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromHeader][Required] int id)
        {
            var response = await _matchService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
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
        [ProducesResponseType(typeof(MatchExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromBody][Required] Match match)
        {
            var response = (Match)(await _matchService.PostAsync(match));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
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
        [ProducesResponseType(typeof(MatchExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromHeader][Required] int id, [FromBody][Required] Match match)
        {
            var response = (Match)(await _matchService.UpdateAsync(id, match));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
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
        public async Task<IActionResult> DeleteAsync([FromHeader][Required] int id) => Ok(await _matchService.DeleteAsync(id));
    }
}
