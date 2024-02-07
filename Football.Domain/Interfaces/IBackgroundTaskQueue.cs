namespace Football.Domain.Interfaces
{
    using System.Threading.Tasks;
    using System.Threading;
    using System;

    public interface IBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(
            CancellationToken cancellationToken);
    }
}
