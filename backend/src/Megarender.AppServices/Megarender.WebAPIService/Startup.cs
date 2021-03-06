using Megarender.Business;
using Megarender.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Megarender.WebAPIService.Models;
using Megarender.DataBus;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Prometheus;
using Masking.Serilog;
using Megarender.DataStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Megarender.WebAPIService
{
    public class Startup {
        private readonly string CorsPolicy = nameof (CorsPolicy);
        public IConfiguration Configuration { get; }

        public Startup (IConfiguration configuration) 
        {
            Configuration = configuration;          
        }

        private JsonSerializerSettings ConfigureJSON() 
        {
            var result = new JsonSerializerSettings () {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore                
            };
            result.Converters.Add(new StringEnumConverter());
            return result;
        }

        public void ConfigureServices (IServiceCollection services) 
        {
            JsonConvert.DefaultSettings = ConfigureJSON;
            services.Configure<ApplicationOptions> (Configuration.GetSection (nameof (ApplicationOptions)));
            
            var applicationOptions = new WebAPIService.Models.ApplicationOptions ();
            Configuration.GetSection (nameof (WebAPIService.Models.ApplicationOptions)).Bind (applicationOptions);            
            
            services.AddSwagger (applicationOptions.IdentityServiceURI);
            services.AddAuth (applicationOptions.IdentityServiceURI);            
            services.AddSQL (Configuration.GetConnectionString ("DefaultConnection"));
            services.AddQueueService (applicationOptions.RabbitMQSeriveURI);
            services.AddDataStorage(Configuration);
            services.AddBusinessServices();

            services.AddSingleton<MetricReporter>();

            services.AddControllers ()
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.AddCors (options => {
                options.AddPolicy (nameof (CorsPolicy),
                    builder => builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .Build ());
            });
            services.AddHealthChecks();
        }

        public void Configure (IApplicationBuilder app, IApiVersionDescriptionProvider provider, IHostEnvironment env) 
        {
            Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (Configuration)
                                .Destructure.ByMaskingProperties("Password", "Token")
                                .WriteTo.Seq(System.Environment.GetEnvironmentVariable(nameof(Models.EnvironmentVariables.SeqURL)))
                                .CreateLogger();
             
            if (!env.IsDevelopment())
            {
                app.UseAPIMiddlewares();
                app.UseMetricServer(); 
                app.UseCustomExceptionHandler();
                app.UseHttpMetrics();
            }
            else {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles(); 
            app.UseCors (nameof (CorsPolicy));
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                foreach ( var description in provider.ApiVersionDescriptions )
                {
                    c.SwaggerEndpoint ($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    c.RoutePrefix = string.Empty;                    
                }
                
            });

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
                endpoints.MapHealthChecks("/health");
            });
            app.ApplyMigrations<APIContext>();
        }
    }
}