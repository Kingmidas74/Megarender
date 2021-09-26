using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Megarender.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSQL (this IServiceCollection services, string connectionString) {
            services.AddDbContextPool<APIContext> ((provider, options) => {                                     
                if(!File.Exists(Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_USER_FILE)))) throw new FileNotFoundException(nameof (EnvironmentVariables.DB_USER_FILE));
                if(!File.Exists(Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PWD_FILE)))) throw new FileNotFoundException(nameof (EnvironmentVariables.DB_PWD_FILE));
                options.UseNpgsql (
                    string.Format (connectionString,
                                    Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_HOST)), 
                                    Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PORT)), 
                                    File.ReadAllText(Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_USER_FILE))), 
                                    File.ReadAllText(Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PWD_FILE)))
                    ), 
                    providerOptions => {
                                providerOptions.MigrationsAssembly ($"{nameof(Megarender)}.{nameof(DataAccess)}");
                    });
                var extension = options.Options.FindExtension<CoreOptionsExtension> ();
                var loggerFactory = extension?.ApplicationServiceProvider.GetService<ILoggerFactory> ();
                if (loggerFactory != null)
                {
                    options.UseLoggerFactory (loggerFactory);
                }
            });
            services.AddScoped<IAPIContext>(provider => provider.GetService<APIContext>());
            return services;
        }
    }
}