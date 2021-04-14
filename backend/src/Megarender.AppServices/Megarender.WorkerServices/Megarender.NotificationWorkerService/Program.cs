using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Megarender.NotificationWorkerService.Models;
using RabbitMQ.Client;
using Serilog;

namespace Megarender.NotificationWorkerService {
    public class Program {
        public static int Main (string[] args) {
            try {
                Console.OutputEncoding = Encoding.UTF8;
                CreateHostBuilder (args).Build ().Run ();
                return 0;
            } catch (Exception ex) {
                Console.WriteLine ($"Host terminated unexpectedly. {ex.Message}");
                return 1;
            } finally {
                Log.CloseAndFlush ();
            }
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host
            .CreateDefaultBuilder ()
            .UseContentRoot (Directory.GetCurrentDirectory ())
            .ConfigureAppConfiguration ((hostingContext, config) => {
                var env = hostingContext.HostingEnvironment;
                config.AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
                    .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true, reloadOnChange : true);

                config.AddEnvironmentVariables ();
                if (args != null) {
                    config.AddCommandLine (args);
                }
            })
            .ConfigureServices ((hostContext, services) => {
                IConfiguration configuration = hostContext.Configuration;
                var rabbitMQSettings = new RabbitMQSettings ();
                var settings = configuration.GetSection (nameof (RabbitMQSettings)).Get<RabbitMQSettings>();                
                configuration.GetSection (nameof (RabbitMQSettings)).Bind (rabbitMQSettings);        
                var rabbitMQSeriveURI = string.Format (settings.RabbitMQServiceURI, System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_USER_FILE)), System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_PWD_FILE)), System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_HOST)), System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.RMQ_PORT)));

                services.AddTransient<MessageService> (s => {
                    var factory = new ConnectionFactory {
                    Uri = new Uri (rabbitMQSeriveURI),
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds (10)
                    };
                    var connection = factory.CreateConnection ();
                    var channel = connection.CreateModel ();
                    foreach (var queue in rabbitMQSettings.Queues) {
                        channel.QueueDeclare (queue: queue.QueueName,
                            durable: queue.Durable,
                            exclusive: false,
                            autoDelete: false);
                    }
                    foreach (var bind in rabbitMQSettings.Bindings) {
                        channel.QueueBind (bind.QueueName, bind.ExchangeName, bind.RoutingKey);
                    }
                    return new MessageService (channel, rabbitMQSettings);
                });
                Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (configuration).CreateLogger ();
                services.AddHostedService<Worker> ();
            })
            .UseSerilog ();
    }
}