using Megarender.WebServiceCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Megarender.ManagementService
{
    public class Startup:StartupBase<Startup> {

        public Startup (IConfiguration configuration):base(configuration) {}

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