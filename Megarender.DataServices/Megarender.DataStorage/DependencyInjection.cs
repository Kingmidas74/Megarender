using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }    
}
