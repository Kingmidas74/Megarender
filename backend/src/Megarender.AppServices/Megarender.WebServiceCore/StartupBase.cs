using System;
using Masking.Serilog;
using Megarender.Features;
using Megarender.DataAccess;
using Megarender.DataBus;
using Megarender.DataStorage;
using Megarender.WebServiceCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Prometheus;
using Serilog;
using EnvironmentVariables = Megarender.WebServiceCore.Models.EnvironmentVariables;

namespace Megarender.WebServiceCore
{
    public class StartupBase<T> {
        protected readonly string CorsPolicy = nameof (CorsPolicy);
        protected IConfiguration Configuration { get; }

        protected StartupBase (IConfiguration configuration) 
        {
            Configuration = configuration;          
        }

        private JsonSerializerSettings ConfigureJSON() 
        {
            var result = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore                
            };
            result.Converters.Add(new StringEnumConverter());
            return result;
        }

        protected void ConfigureServicesBase(IServiceCollection services) 
        {
            JsonConvert.DefaultSettings = ConfigureJSON;
            
            var applicationOptions = new ApplicationOptions ();
            Configuration.GetSection (nameof (ApplicationOptions)).Bind (applicationOptions);
            services.AddSingleton(applicationOptions);
            
            services.AddSwagger<T>(applicationOptions.IdentityServiceURI);
            services.AddAuth (applicationOptions.IdentityServiceURI);            
            services.AddSQL (Configuration.GetConnectionString ("DefaultConnection"));
            services.AddQueueService (Configuration);
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

        protected void ConfigureBase (IApplicationBuilder app, IApiVersionDescriptionProvider provider, IHostEnvironment env) 
        {
            Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (Configuration)
                                .Destructure.ByMaskingProperties("Password", "Token")
                                .WriteTo.Seq(Environment.GetEnvironmentVariable(nameof(EnvironmentVariables.SeqURL)))
                                .CreateLogger();
             
            if (!env.IsDevelopment())
            {
                app.UseAPIMiddlewares();
                app.UseMetricServer(); 
                app.UseCustomExceptionHandler();
                app.UseHttpMetrics();
            }
            else {
                app.ApplyMigrations<APIContext>();
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
        }
    }
}