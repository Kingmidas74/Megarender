using System;
using System.Threading;
using System.Threading.Tasks;
using Megarender.DataBus;
using Megarender.DataBus.Models;
using Megarender.Telegram;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Megarender.TelegramWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IMessageConsumerService _consumerService;
        private readonly IBotService _botService;
        
        private readonly ILogger<Worker> _logger;

        private Task<bool> SendMessageToTelegramEventHandler(SendMessageToTelegramEvent message)
        {
            return _botService.SendTextMessageAsync(Int32.Parse(message.TelegramId),message.Variables["Code"]);
        }

        public Worker(ILogger<Worker> logger, IMessageConsumerService consumerService, IBotService botService)
        {
            _logger = logger;
            _consumerService = consumerService;
            _botService = botService;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await Task.Run (() => _consumerService.Subscribe(envelope =>  envelope switch
            {
                Envelope<SendMessageToTelegramEvent> m => SendMessageToTelegramEventHandler(m.Message).GetAwaiter().GetResult(),
                _ => false
            }), stoppingToken);
        }
    }
}