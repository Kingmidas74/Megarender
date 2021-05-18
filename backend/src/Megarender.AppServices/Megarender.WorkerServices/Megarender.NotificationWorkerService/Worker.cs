using System;
using System.Threading;
using System.Threading.Tasks;
using Megarender.DataBus;
using Megarender.DataBus.Models;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Megarender.NotificationWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IMessageConsumerService _consumerService;

        public Worker(IMessageConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await Task.Run (() => _consumerService.Subscribe<SendCodeMessage>(envelope => {
                Log.Logger.Information ($"Worker running at: {DateTimeOffset.Now}. Message: {envelope.Message.UserId}, {envelope.Message.Code}");
                return true;
            }), stoppingToken);
        }
    }
}