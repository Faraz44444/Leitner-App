using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Service;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;

namespace Core.Infrastructure.ScheduledJob
{
    public abstract class BaseScheduledJob : BaseScopedJob
    {
        private readonly CrontabSchedule _Schedule;
        private DateTime _NextRun;
        protected abstract string Schedule { get; }

        public ServiceContext NewServices => AppContext.Current.Services;

        public BaseScheduledJob(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _Schedule = CrontabSchedule.Parse(Schedule);
            _NextRun = _Schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                if (now > _NextRun)
                {
                    await ProcessJob();
                    _NextRun = _Schedule.GetNextOccurrence(DateTime.Now);
                }

                await Task.Delay(5000, stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
        }

    }
}
