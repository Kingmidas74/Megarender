using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Megarender.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSQL (this IServiceCollection services, string connectionString) {
            services.AddDbContextPool<APIContext> ((provider, options) => {                     
                if(!File.Exists(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_USER)))) throw new FileNotFoundException(nameof (EnvironmentVariables.API_DB_USER));
                if(!File.Exists(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_PASSWORD)))) throw new FileNotFoundException(nameof (EnvironmentVariables.API_DB_PASSWORD));
                options.UseNpgsql (
                    string.Format (connectionString, 
                                    System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_HOST)), 
                                    System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_PORT)), 
                                    File.ReadAllText(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_USER))), 
                                    File.ReadAllText(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.API_DB_PASSWORD)))), 
                    providerOptions => {
                                providerOptions.EnableRetryOnFailure (3);
                                providerOptions.MigrationsAssembly (nameof (DataAccess));
                    });
                var extension = options.Options.FindExtension<CoreOptionsExtension> ();
                if (extension != null) {
                    var loggerFactory = extension.ApplicationServiceProvider.GetService<ILoggerFactory> ();
                    if (loggerFactory != null) {
#if DEBUG
                        options.EnableSensitiveDataLogging ().UseLoggerFactory (loggerFactory);
#else
                        options.UseLoggerFactory (loggerFactory);
#endif
                    }
                };
            });
            services.AddScoped<IAPIContext>(provider => provider.GetService<APIContext>());
            return services;
        }
    }
}