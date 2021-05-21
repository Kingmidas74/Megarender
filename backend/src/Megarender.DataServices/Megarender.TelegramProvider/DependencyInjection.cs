using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Megarender.Telegram
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTelegramProvider (this IServiceCollection services, IConfiguration configuration) 
        {
            var botSettings = new BotSettings();
            configuration.GetSection(nameof(BotSettings)).Bind(botSettings);
            
            if (botSettings.Enabled && !String.IsNullOrEmpty(botSettings.BotToken))
            {    
                botSettings.BotToken = string.Format(botSettings.BotToken, File.ReadAllText(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.TG_TOKEN_FILE))));
                botSettings.WebHookURI = string.Format(botSettings.WebHookURI, Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.WEBHOOK_URI)));

                services.AddSingleton(botSettings);
                services.AddSingleton<IBotService, BotService>();
            }
            else
            {
                services.AddSingleton(botSettings);
                services.AddSingleton<IBotService, DefaultBotService>();
            }
            return services;
        }
    }
}