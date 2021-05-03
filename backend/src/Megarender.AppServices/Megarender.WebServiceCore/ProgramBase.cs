using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Megarender.WebServiceCore {
    public static class ProgramBase {
        public static async Task<int> RunBase<T> (Func<string[],IWebHost> buildWebHost, string[] args)
        where T:StartupBase<T>
        {
            try {
                Console.OutputEncoding = Encoding.UTF8;
                await buildWebHost(args).RunAsync();
                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex,$"Host terminated unexpectedly. {ex.Message}");
                return 1;
            } finally {
                Log.CloseAndFlush ();
            }
        }
        

        public static IWebHost BuildWebHost<T> (string[] args)
            where T:StartupBase<T> 
        {
            return WebHost
            .CreateDefaultBuilder ()
            .UseKestrel ()
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
            .ConfigureLogging ((hostingContext, config) => {
                config.ClearProviders ();
            })
            .UseDefaultServiceProvider ((context, options) => {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment ();
            })

            .UseStartup<T> ()
            .UseSerilog ()
            .Build ();
        }
    }
}