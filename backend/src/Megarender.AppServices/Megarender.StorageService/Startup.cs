using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Serilog;
using Megarender.StorageService.Middleware;
using Megarender.StorageService.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Prometheus;
using Masking.Serilog;
using Megarender.StorageService.DAL;

namespace Megarender.StorageService
{
    public class Startup
    {
        private readonly string CorsPolicy = nameof (CorsPolicy);
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private JsonSerializerSettings ConfigureJSON() {
            var result = new JsonSerializerSettings () {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore                
            };
            result.Converters.Add(new StringEnumConverter());
            return result;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = ConfigureJSON;
            services.Configure<ApplicationOptions> (Configuration.GetSection (nameof (ApplicationOptions)));
            
            var applicationOptions = new Models.ApplicationOptions ();
            Configuration.GetSection (nameof (Models.ApplicationOptions)).Bind (applicationOptions);            
            
            services.AddSwagger (applicationOptions.IdentityServiceURI);
            services.AddAuth (applicationOptions.IdentityServiceURI);            
            services.AddSQL(Configuration.GetConnectionString ("DefaultConnection"));
            
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
        }

        public void Configure (IApplicationBuilder app, IApiVersionDescriptionProvider provider) 
        {
            Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (Configuration)
                                .Destructure.ByMaskingProperties("Password", "Token")
                                .WriteTo.Seq(System.Environment.GetEnvironmentVariable(nameof(Models.EnvironmentVariables.SeqURL)))
                                .CreateLogger();
            
            app.UseMiddleware<RequestResponseLoggingMiddleware> ();
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseMiddleware<CountRequestMiddleware>();
                     

            app.UseCors (nameof (CorsPolicy));
            app.UseMetricServer(); 
            app.UseCustomExceptionHandler();
            app.UseHttpMetrics();
            app.UseDeveloperExceptionPage();  
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
            });
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory> ().CreateScope ()) {
                var context = serviceScope.ServiceProvider.GetRequiredService<StorageDbContext> ();                    
                    
                if(!context.AllMigrationsApplied())
                {
                    context.Database.Migrate();
                }                
            }
        }
    }
}
