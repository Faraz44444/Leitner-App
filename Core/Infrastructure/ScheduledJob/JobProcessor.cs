using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Core.Infrastructure.ScheduledJob
{
    public abstract class JobProcessor : IHostedService
    {
        private Task _ExecutingTask;
        private readonly CancellationTokenSource _CancellationTokenSrc = new CancellationTokenSource();

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _ExecutingTask = ExecuteAsync(_CancellationTokenSrc.Token);

            return _ExecutingTask.IsCompleted ? _ExecutingTask : Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_ExecutingTask == null) return;

            try
            {
                _CancellationTokenSrc.Cancel();
            }
            finally
            {
                await Task.WhenAny(
                    _ExecutingTask,
                    Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected virtual async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            do
            {
                await ProcessJob();
                await Task.Delay(5000, cancellationToken);
            } while (!cancellationToken.IsCancellationRequested);
        }

        protected abstract Task ProcessJob();
    }
}
