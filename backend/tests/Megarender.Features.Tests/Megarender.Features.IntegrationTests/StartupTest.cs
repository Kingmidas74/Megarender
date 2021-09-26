using Megarender.WebServiceCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Megarender.Features.IntegrationTests
{
    public class StartupTest:StartupBase<StartupTest>
    {
        public StartupTest(IConfiguration configuration) : base(configuration)
        {
        }
        
        public void ConfigureServices (IServiceCollection services) 
        {
            ConfigureServicesBase(services);
        }

        public void Configure (IApplicationBuilder app, IApiVersionDescriptionProvider provider, IHostEnvironment env) 
        {
            ConfigureBase(app,provider,env);
        } 
    }
}