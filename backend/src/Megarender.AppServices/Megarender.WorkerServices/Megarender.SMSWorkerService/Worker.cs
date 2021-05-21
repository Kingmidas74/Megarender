using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Megarender.DataBus;
using Megarender.DataBus.Models;
using Megarender.SMS;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Megarender.SMSWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IMessageConsumerService _consumerService;
        
        private readonly ILogger<Worker> _logger;
        private readonly ISMSService _smsService;

        private Task<bool> SendSMSEventHandler(SendSMSEvent message)
        {
            return _smsService.SendMessageAsync(message.Phone,message.Variables["Code"]);
        }

        public Worker(ILogger<Worker> logger, IMessageConsumerService consumerService, ISMSService smsService)
        {
            _logger = logger;
            _consumerService = consumerService;
            _smsService = smsService;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await Task.Run (() => _consumerService.Subscribe(envelope =>  envelope switch
            {
                Envelope<SendSMSEvent> m => SendSMSEventHandler(m.Message).GetAwaiter().GetResult(),
                _ => false
            }), stoppingToken);
        }
    }
}