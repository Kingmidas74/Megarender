﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Megarender.DataBus.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Megarender.DataBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddQueueService (this IServiceCollection services, IConfiguration configuration) 
        {
            var rmqSettings = new RMQSettings();
            configuration.GetSection(nameof(RMQSettings)).Bind(rmqSettings);
            if (rmqSettings.Enabled && !String.IsNullOrEmpty(rmqSettings.ConntectionString))
            {
                services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
                services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitPooledObjectPolicy>();
                
                
                rmqSettings.ConntectionString = string.Format(rmqSettings.ConntectionString,
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_USER_FILE)),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_PWD_FILE)),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_HOST)),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_PORT)));

                services.AddSingleton(rmqSettings);
                services.AddSingleton<IMessageProducerService, RMQMessageProducerService>();
                // services.AddTransient (s => {
                //     var factory = new ConnectionFactory {
                //         Uri = new Uri (rmqSettings.ConntectionString)
                //     };
                //     var connection = factory.CreateConnection ();
                //     var channel = connection.CreateModel ();
                //     return new MessageProducerService (channel);
                // });
            }
            else
            {
                services.AddSingleton<IMessageProducerService, RMQMessageProducerService>();
            }
            return services;
        }
    }
}
