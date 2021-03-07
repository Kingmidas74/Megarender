using Microsoft.Extensions.DependencyInjection;
using System;
using RabbitMQ.Client;

namespace Megarender.DataBus
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddQueueService (this IServiceCollection services, string connectionString) {
            
            var rabbitMQSeriveURI = string.Format (connectionString, 
                        System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_USER_FILE)), 
                        System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_PWD_FILE)), 
                        System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_HOST)), 
                        System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_PORT)));
            
            services.AddTransient<MessageProducerService> (s => {
                var factory = new ConnectionFactory {
                    Uri = new Uri (rabbitMQSeriveURI)
                };
                var connection = factory.CreateConnection ();
                var channel = connection.CreateModel ();
                return new MessageProducerService (channel);
            });
            return services;
        }
    }
}
