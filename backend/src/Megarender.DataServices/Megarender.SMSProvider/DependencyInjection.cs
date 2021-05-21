using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Megarender.SMS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSMSProvider (this IServiceCollection services, IConfiguration configuration) 
        {
            var smsSettings = new SMSSettings();
            configuration.GetSection(nameof(SMSSettings)).Bind(smsSettings);
            
            if (smsSettings.Enabled && !String.IsNullOrEmpty(smsSettings.Token))
            {    
                smsSettings.Token = string.Format(smsSettings.Token, File.ReadAllText(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.TOKEN))));
                smsSettings.AccountSID = string.Format(smsSettings.AccountSID, File.ReadAllText(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.ACCOUNTSID))));
                smsSettings.Sender = string.Format(smsSettings.Sender, Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.SENDER)));
                
                services.AddSingleton(smsSettings);
                services.AddSingleton<ISMSService, TwillioService>();
            }
            else
            {
                services.AddSingleton(smsSettings);
                services.AddSingleton<ISMSService, DefaultSMSService>();
            }
            return services;
        }
    }
}