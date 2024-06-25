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

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;

        public ManagerController(IManagerService managerService, IMapper mapper) {
            _managerService = managerService;
            _mapper = mapper;
        }

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
        [ProducesResponseType(typeof(IEnumerable<ManagerFootball>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManagersAsync() => Ok(await _managerService.GetManagersAsync());

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
        [ProducesResponseType(typeof(ManagerFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetManagerByIdAsync([FromHeader][Required] int id)
        {
            var response = await _managerService.GetManagerByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<ManagerFootballExemple>(response));
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
        [ProducesResponseType(typeof(ManagerFootball), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateManagerAsync([FromBody][Required] ManagerFootballExemple manager)
        {
           var response = await _managerService.CreateManagerAsync(_mapper.Map<ManagerFootball>(manager));
            if (response == null)
                return NotFound();
            return Ok(response);
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
        [ProducesResponseType(typeof(ManagerFootballExemple), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateManagerAsync([FromHeader][Required] int id,[FromBody][Required] ManagerFootballExemple manager)
        {
            var response = await _managerService.UpdateManagerAsync(id, _mapper.Map<ManagerFootball>(manager));
            if (response == null)
                return NotFound();
            return Ok(_mapper.Map<ManagerFootballExemple>(response));
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
        public async Task<IActionResult> DeleteManagerAsync([FromHeader][Required] int id) => Ok(await _managerService.DeleteManagerAsync(id));
    }
}
