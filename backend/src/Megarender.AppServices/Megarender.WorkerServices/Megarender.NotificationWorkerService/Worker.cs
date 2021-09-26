using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Megarender.DataBus;
using Megarender.DataBus.Enums;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
using Microsoft.Extensions.Hosting;

namespace Megarender.NotificationWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IMessageConsumerService _consumerService;
        private readonly IMessageProducerService _producerService;
        
        private bool UserRegistratedEventHandler(UserRegistratedEvent message)
        {
            _producerService.Enqueue(new SendEmailEvent
            {
                Email = string.Empty,
                Reason = nameof(UserRegistratedEvent)
            }, new Dictionary<string, string>
            {
                {DefaultHeaders.Parent.GetDescription(), nameof(UserRegistratedEvent)}
            });
            return true;
        }
        
        private bool CodeGeneratedEventHandler(CodeGeneratedEvent message)
        {
            _producerService.Enqueue(new SendMessageToTelegramEvent
            {
                TelegramId = string.Empty,
                Reason = nameof(CodeGeneratedEvent),
                Variables = new Dictionary<string, string>
                {
                    {nameof(CodeGeneratedEvent.Code), message.Code}
                }
            },new Dictionary<string, string>
            {
                {DefaultHeaders.Parent.GetDescription(), nameof(CodeGeneratedEvent)}
            });
            _producerService.Enqueue(new SendSMSEvent
            {
                Phone = string.Empty,
                Reason = nameof(CodeGeneratedEvent),
                Variables = new Dictionary<string, string>
                {
                    {nameof(CodeGeneratedEvent.Code), message.Code}
                }
            },new Dictionary<string, string>
            {
                {DefaultHeaders.Parent.GetDescription(), nameof(CodeGeneratedEvent)}
            });
            return true;
        }

        public Worker(IMessageConsumerService consumerService, IMessageProducerService producerService)
        {
            _consumerService = consumerService;
            _producerService = producerService;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await Task.Run (() => _consumerService.Subscribe(envelope => envelope switch
            {
                Envelope<UserRegistratedEvent> m => UserRegistratedEventHandler(m.Message),
                Envelope<CodeGeneratedEvent> m => CodeGeneratedEventHandler(m.Message),
                _ => false
            }), stoppingToken);
        }
    }
}