using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.ScheduledJob
{
    /// <summary>
    /// Used to create background jobs. Jobs that should run parallel to the application.
    /// </summary>
    public abstract class BaseScopedJob : JobProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BaseScopedJob(IServiceScopeFactory serviceScopeFactory) : base()
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ProcessJob()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                await Execute(scope.ServiceProvider);
            }
        }
        /// <summary>
        /// The job that should be executed
        /// </summary>
        public abstract Task Execute(IServiceProvider serviceProvider);
    }
}
