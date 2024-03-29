﻿using System;
using System.IO;
using Megarender.DataBus.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            
            if (rmqSettings.Enabled && !String.IsNullOrEmpty(rmqSettings.ConnectionString))
            {    
                rmqSettings.ConnectionString = string.Format(rmqSettings.ConnectionString,
                    File.ReadAllText(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_USER_FILE))),
                    File.ReadAllText(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_PWD_FILE))),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_HOST)),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.RMQ_PORT)));

                services.AddSingleton(rmqSettings);
                
                services.AddSingleton<DefaultObjectPoolProvider>();
                services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitPooledObjectPolicy>();
                services.AddSingleton<IMessageProducerService, RMQMessageProducerService>();
                services.AddSingleton<IMessageConsumerService, RMQMessageConsumerService>();
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
                services.AddSingleton(rmqSettings);
                services.AddSingleton<IMessageProducerService, DefaultMessageProducerService>();
                services.AddSingleton<IMessageConsumerService, DefaultMessageConsumerService>();
            }
            return services;
        }
    }
}
