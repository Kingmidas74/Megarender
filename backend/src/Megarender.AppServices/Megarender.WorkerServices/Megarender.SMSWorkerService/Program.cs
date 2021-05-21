using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Megarender.DataBus;
using Megarender.SMS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Megarender.SMSWorkerService
{
    public class Program {
        public static async Task<int> Main (string[] args) {
            var ctx = new CancellationToken();
            try {
                Console.OutputEncoding = Encoding.UTF8;
                
                await CreateHostBuilder(args).Build().RunAsync(ctx);
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
                    config.AddJsonFile ("appsettings.json", optional : true, reloadOnChange : false)
                        .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true, reloadOnChange : false);

                    config.AddEnvironmentVariables ();
                    if (args != null) {
                        config.AddCommandLine (args);
                    }
                })
                .ConfigureServices ((hostContext, services) => {
                    var configuration = hostContext.Configuration;
                    services.AddQueueService(configuration);
                    services.AddSMSProvider(configuration);
                    Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (configuration).CreateLogger ();
                    services.AddHostedService<Worker> ();
                })
                .UseSerilog ();
    }
}