using Microsoft.Extensions.Logging;
using NServiceBus;
using RobertMQ.Application;

namespace RobertMQ.SenderWorker
{
    public class SenderWorker : BackgroundService
    {
        private readonly ILogger<SenderWorker> _logger;
        private readonly IMessageSession _messageSession;
        public SenderWorker(ILogger<SenderWorker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var round = 0;
                while (!stoppingToken.IsCancellationRequested)
                {
                    await _messageSession.Send(new Ping { Round = round++ })
                    .ConfigureAwait(false);

                    _logger.LogInformation($"Message #{round}");

                    await Task.Delay(1_000, stoppingToken)
                        .ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException)
            {
                // graceful shutdown
            }
        }
    }
}