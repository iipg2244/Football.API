//namespace Football.API.Controllers.v1
//{
//    using Football.Domain.Contracts;
//    using Microsoft.AspNetCore.Http;
//    using Microsoft.AspNetCore.Mvc;
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.Threading.Tasks;
//    using Football.Domain.Entities.Exemples;
//    using Football.Domain.Entities.Football;

//    [ApiController]
//    [ApiVersion("1.0")]
//    [Route("/api/v{version:apiVersion}/[controller]")]
//    public class RefereeController : ControllerBase
//    {
//        private readonly IRefereeService _refereeService;

//        public RefereeController(IRefereeService refereeService) => _refereeService = refereeService;

//        /// <summary>
//        /// Gets list of referees.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <returns>List of referees.</returns>
//        /// <response code="200">Ok.</response>
//        [HttpGet]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(List<RefereeExemple>), StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetAsync() => Ok(await _refereeService.GetAsync());

//        /// <summary>
//        /// Get referee by id.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <param name="id">Referee identifier.</param>
//        /// <returns>If found, returns Referee otherwise null.</returns>
//        /// <response code="200">Ok.</response>
//        /// <response code="404">Not found.</response>
//        [HttpGet("options")]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(RefereeExemple), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> GetByIdAsync([FromHeader][Required] int id)
//        {
//            var response = await _refereeService.GetByIdAsync(id);
//            if (response == null)
//                return NotFound();
//            return Ok(response);
//        }

//        /// <summary>
//        /// Insert referee.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <param name="referee">Referee to insert.</param>
//        /// <returns>If the insert is correct, returns Referee otherwise null.</returns>
//        /// <response code="200">Ok.</response>
//        /// <response code="404">Not found.</response>
//        [HttpPost("options")]
//        [Consumes("application/json")]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(RefereeExemple), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> PostAsync([FromBody][Required] Referee referee)
//        {
//            var response = (Referee)(await _refereeService.PostAsync(referee));
//            if (response == null)
//                return NotFound();
//            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
//        }

//        /// <summary>
//        /// Update referee by id.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <param name="id">Referee identifier.</param>
//        /// <param name="referee">Referee with the changes to be made.</param>
//        /// <returns>If the update is correct, returns Referee otherwise null.</returns>
//        /// <response code="200">Ok.</response>
//        /// <response code="404">Not found.</response>
//        [HttpPut("options")]
//        [Consumes("application/json")]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(RefereeExemple), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(Nullable), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> UpdateAsync([FromHeader][Required] int id, [FromBody][Required] Referee referee)
//        {
//            var response = (Referee)(await _refereeService.UpdateAsync(id, referee));
//            if (response == null)
//                return NotFound();
//            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Id }, response);
//        }

//        /// <summary>
//        /// Delete referee by id.
//        /// </summary>
//        /// <remarks>
//        /// # Markdown
//        /// </remarks>
//        /// <param name="id">Referee identifier.</param>
//        /// <returns>If the delete is correct, returns true otherwise false.</returns>
//        /// <response code="200">Ok.</response>
//        [HttpDelete("options")]
//        [Produces("application/json")]
//        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
//        public async Task<IActionResult> DeleteAsync([FromHeader][Required] int id) => Ok(await _refereeService.DeleteAsync(id));
//    }
//}
