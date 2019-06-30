using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// https://www.tutorialdocs.com/article/dotnet-generic-host.html
namespace backgroundservice.Services
{
    public class ListenerService : BackgroundService, IListenerService
    {
        private readonly ILogger<ListenerService> _logger;

        List<Task> tasks = new List<Task>();

        private int i = 0;

        private CancellationToken StoppingToken { get; set; }

        public ListenerService(ILogger<ListenerService> servicelogger, 
                               IConfiguration configuration,
                               IOptions<Setting> options)
        {
            _logger = servicelogger;
            string connectionString = options.Value.ConnectionString;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StoppingToken = stoppingToken;

            stoppingToken.Register(() => _logger.LogDebug("Demo Service is stopping."));

            for (int j = 0; j < 2; j++)
            {
                await CreateNewListerner();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Demo Service is running in background");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

        public async Task CreateNewListerner()
        {
            Task task = await Task.Factory.StartNew(async () =>
            {
                var lst1 = new Listener(++i);
                await lst1.Start(StoppingToken);
            }, StoppingToken); // TODO: handle exceptions in a ContinueWith block for each Task and log exceptions.

            tasks.Add(task);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Printer2 is stopped");
            base.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }
    }
}
