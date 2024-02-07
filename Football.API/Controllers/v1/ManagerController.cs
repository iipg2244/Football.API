namespace Football.API.Controllers.v1
{
    using Football.Infrastructure;
    using Football.Domain.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Football.Domain.Entities.Exemples;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService) => _managerService = managerService;

        /// <summary>
        /// Gets list of managers.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <returns>List of managers.</returns>
        /// <response code="200">Ok.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<ManagerExemple>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync() => Ok(await _managerService.GetAsync());

        /// <summary>
        /// Get manager by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Manager identifier.</param>
        /// <returns>If found, returns Manager otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ManagerExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromHeader][Required] int id)
        {
            var response = await _managerService.GetByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Insert manager.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="manager">Manager to insert.</param>
        /// <returns>If the insert is correct, returns Manager otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPost("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ManagerExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostAsync([FromHeader][Required] Manager manager)
        {
           var response = (Manager)(await _managerService.PostAsync(manager));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        /// <summary>
        /// Update manager by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Manager identifier.</param>
        /// <param name="manager">Manager with the changes to be made.</param>
        /// <returns>If the update is correct, returns Manager otherwise null.</returns>
        /// <response code="200">Ok.</response>
        /// <response code="404">Not found.</response>
        [HttpPut("options")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ManagerExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromHeader][Required] int id,[FromBody][Required] Manager manager)
        {
            var response = (Manager)(await _managerService.UpdateAsync(id, manager));
            if (response == null)
                return NotFound();
            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
        }

        /// <summary>
        /// Delete manager by id.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="id">Manager identifier.</param>
        /// <returns>If the delete is correct, returns true otherwise false.</returns>
        /// <response code="200">Ok.</response>
        [HttpDelete("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromHeader][Required] int id) => Ok(await _managerService.DeleteAsync(id));
    }
}
