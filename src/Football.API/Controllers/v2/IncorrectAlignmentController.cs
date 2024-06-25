namespace Football.API.Controllers.v2
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [ApiController]
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/[controller]")] 
    public class IncorrectAlignmentController : ControllerBase
    {
        private readonly ILogger<IncorrectAlignmentController> _logger;

        public IncorrectAlignmentController(ILogger<IncorrectAlignmentController> logger) => _logger = logger;

        /// <summary>
        /// Generate a background task.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <returns>Not Implemented Exception.</returns>
        /// <response code="501">Not Implemented.</response>
        [MapToApiVersion("2.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Get()
        {
            _logger.LogInformation("Using serilog with dependencies.");
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

        /// <summary>
        /// Find task ids.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <param name="listId">List with ids of tasks.</param>
        /// <returns>Not Implemented Exception.</returns>
        /// <response code="501">Not Implemented.</response>
        [MapToApiVersion("2.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Post([FromBody][Required] List<int> listId)
        {
            Log.Information("Using serilog without dependencies.");
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status501NotImplemented);
            }
        }

    }
}
