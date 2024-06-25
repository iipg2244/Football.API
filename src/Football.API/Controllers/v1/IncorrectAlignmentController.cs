namespace Football.API.Controllers.v1
{
    using Football.Domain.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")] 
    public class IncorrectAlignmentController : ControllerBase
    {
        public IBackgroundTaskQueue _queue { get; }
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IncorrectAlignmentController(IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        /// <summary>
        /// Generate a background task.
        /// </summary>
        /// <remarks>
        /// # Markdown
        /// </remarks>
        /// <returns>Progress message.</returns>
        /// <response code="200">Ok.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    //The Job
                    await Task.Delay(TimeSpan.FromSeconds(5), token);
                }
            });
            return Ok("In progress..");
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Post([FromBody][Required] List<int> listId)
        {
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
