using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerService
{
    internal class TestAsyncSonar : IHostedService
    {
        #region Const
        private const int TIMER_DELAY = 2000;
        private const int TIMER_PERIODE = 60 * 1000;
        #endregion 

        #region Fields
        private readonly ILogger<TestAsyncSonar> logger;
        private readonly Timer timer;
        #endregion

        #region C'tor
        internal TestAsyncSonar(ILogger<TestAsyncSonar> logger)
        {
            this.logger = logger;
            this.logger.LogInformation("SystemStatus provider is ready...");
        }
        #endregion

        #region IResourceProvider
        public event EventHandler<IList<int>>? OnResource;

        #endregion

        #region IHostedService

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(TIMER_DELAY, TIMER_PERIODE);
            logger.LogInformation("Periodic system status provider started...");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
            logger.LogInformation("Periodic system status provider stopped...");
            return Task.CompletedTask;
        }

        #endregion


    }
}