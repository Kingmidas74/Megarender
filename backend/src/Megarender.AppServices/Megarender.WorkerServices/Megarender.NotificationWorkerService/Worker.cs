using System;
using System.Collections.Generic;
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
        
        // private readonly Dictionary<Type, Func<IEvent,bool>> _handlers = new()
        // {
        //     { typeof(UserRegistratedEvent), UserRegistratedEventHandler },
        //     { typeof(CodeGeneratedEvent), CodeGeneratedEventHandler }
        // };

        private bool UserRegistratedEventHandler(UserRegistratedEvent message)
        {
            Log.Logger.Information($"This is message type {message.GetType()}");
            return true;
        }
        
        private bool CodeGeneratedEventHandler(IEvent message)
        {
            Log.Logger.Information($"This is message type {message.GetType()}");
            return true;
        }

        public Worker(IMessageConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {
            await Task.Run (() => _consumerService.Subscribe(envelope => {
                if(envelope.GetType() == typeof(Envelope<UserRegistratedEvent>))
                {
                    throw new Exception();
                }
                if(envelope.GetType() == typeof(Envelope<CodeGeneratedEvent>))
                {
                    throw new Exception();
                }
                return false;
            }), stoppingToken);
        }
    }
}