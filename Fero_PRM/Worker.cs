using FeroPRMData.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace NotificationWorkerService
{
    public class Worker : BackgroundService
    {
        private static readonly int IDLE_MINUTES = 4;
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        public IServiceProvider Services { get; }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var _notificationService = scope.ServiceProvider
                            .GetRequiredService<INotificationService>();
                    await _notificationService.ScanCasting();
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(IDLE_MINUTES * 60 * 1000, stoppingToken);
            }
        }
    }
}
