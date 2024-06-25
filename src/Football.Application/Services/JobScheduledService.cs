namespace Football.Application.Services
{
    using System.Threading.Tasks;
    using System.Threading;
    using System;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Football.Domain.Contracts;

    public class JobScheduledService : BackgroundService
    {
        private readonly ILogger<JobScheduledService> _logger;

        public JobScheduledService(IBackgroundTaskQueue backgroundTaskQueue,
            ILogger<JobScheduledService> logger)
        {
            TaskQueue = backgroundTaskQueue;
            _logger = logger;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        protected async override Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            _logger.LogInformation("Queued Hosted Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                       "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }

            _logger.LogInformation("Queued Hosted Service is stopping.");
        }
    }
}
