using System;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Megarender.DataStorage
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ICommandStore), typeof(CommandStore));

            var azureStoreConfig = configuration.GetValue<AzureStorageOptions>(nameof(AzureStorageOptions));
            if(azureStoreConfig!=null) {                
                services.AddSingleton(x=>new BlobServiceClient(azureStoreConfig.ConnectionString));
                services.AddSingleton<IFileStorage,AzureStorage>();
            }

            var ftpStoreConfig = configuration.GetValue<FTPStorageOptions>(nameof(FTPStorageOptions)); 
            if(ftpStoreConfig!=null) {
                //services.AddSingleton<IFileStorage,FTPStorage>(ftpStoreConfig.RootPath);
            }

            var redisSettings = new RedisSettings();
            configuration.GetSection(nameof(RedisSettings)).Bind(redisSettings);
            if (!redisSettings.Enabled && !String.IsNullOrEmpty(redisSettings.ConnectionString))
            {
                redisSettings.ConnectionString = string.Format(redisSettings.ConnectionString,
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.REDIS_HOST)),
                    Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.REDIS_PORT)));
                services.AddSingleton(redisSettings);

                services.AddStackExchangeRedisCache(options =>
                {
                    options.ConfigurationOptions = ConfigurationOptions.Parse(redisSettings.ConnectionString);
                });
                services.AddSingleton<ICacheDataStorage, RedisDataStorage>();
            }
            else
            {
                services.AddSingleton<ICacheDataStorage, DefaultCacheDataStorage>();
            }

            return services;
        }
    }

    
}
