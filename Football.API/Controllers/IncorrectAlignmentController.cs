using Football.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Football.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class IncorrectAlignmentController : ControllerBase
    {
        public IBackgroundTaskQueue _queue { get; }
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IncorrectAlignmentController(IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
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

        [HttpPost]
        public IActionResult Post([FromBody] List<int> listId)
        {
            throw new NotImplementedException();
        }
        //public async Task<IActionResult> PostAsync()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
