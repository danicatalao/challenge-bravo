using currency_conversion.Core.Interfaces.Services;

namespace currency_conversion.worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly ICurrencyFetch _currencyFetch;

        public Worker(ILogger<Worker> logger, ICurrencyFetch currencyFetch)
        {
            _logger = logger;
            _currencyFetch = currencyFetch;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _currencyFetch.FetchCurrenciesAsync();
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}