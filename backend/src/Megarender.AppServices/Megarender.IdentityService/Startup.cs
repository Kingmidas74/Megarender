using FluentValidation;
using IdentityServer4.Models;
using Masking.Serilog;
using MediatR;
using Megarender.DataBus;
using Megarender.IdentityService.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Prometheus;
using Serilog;

namespace Megarender.IdentityService {
    public class Startup {
        private readonly string _corsPolicy = nameof (_corsPolicy);
        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public Startup (IConfiguration configuration, IWebHostEnvironment environment) {
            Configuration = configuration;
            Environment = environment;
            Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (Configuration).CreateLogger ();
        }

        public void ConfigureServices (IServiceCollection services) {
            services.Configure<ApplicationOptions> (Configuration.GetSection (nameof(ApplicationOptions)));            
            services.AddTransient<UtilsService>();
            services.AddSwagger();
            services.AddQueueService(Configuration);
            services.AddSingleton<MetricReporter>();
            services.AddSQL(Configuration.GetConnectionString ("DefaultConnection"));
            services.AddMediatR(typeof(Startup));
            services.AddValidatorsFromAssembly(typeof(Startup).Assembly);    
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services
                .AddIdentityServer (x => {
                    x.IssuerUri = System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_HOST));
                    x.Events.RaiseErrorEvents = true;
                    x.Events.RaiseInformationEvents = true;
                    x.Events.RaiseFailureEvents = true;
                    x.Events.RaiseSuccessEvents = true;
                })
                .AddInMemoryIdentityResources (Configuration.GetSection ("IdentityService:IdentityResources").Get<IdentityResource[]> ())
                .AddInMemoryApiResources (Configuration.GetSection ("IdentityService:ApiResources").Get<ApiResource[]> ())
                .AddInMemoryApiScopes (Configuration.GetSection ("IdentityService:ApiResources").Get<ApiScope[]> ())
                .AddInMemoryClients (Configuration.GetSection ("IdentityService:Clients").Get<Client[]> ())
                .AddDeveloperSigningCredential ()
                .AddProfileService<ProfileService> ()
                .AddExtensionGrantValidator<PasswordValidator> ();
            services.AddControllers()
                .AddNewtonsoftJson (options => {
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddCors (options => {
                options.AddPolicy (nameof (_corsPolicy),
                    builder => builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .Build ());
            });
        }

        public void Configure (IApplicationBuilder app, IApiVersionDescriptionProvider provider) {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory> ()?.CreateScope ()) {
                if (serviceScope != null)
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext> ();                    
                   
                    if(!context.AllMigrationsApplied())
                    {
                        context.Database.Migrate();
                    }
                }
            }

            
            Log.Logger = new LoggerConfiguration ().ReadFrom.Configuration (Configuration)
                                .Destructure.ByMaskingProperties("Password", "Token")
                                .CreateLogger ();
            
            app.UseMiddleware<RequestResponseLoggingMiddleware> ();
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseMiddleware<CountRequestMiddleware>();

            app.UseCors (nameof (_corsPolicy));
            app.UseMetricServer(); 
            app.UseCustomExceptionHandler();
            app.UseHttpMetrics();
            
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                foreach ( var description in provider.ApiVersionDescriptions )
                {
                    c.SwaggerEndpoint ($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    c.RoutePrefix = string.Empty;                    
                }
                
            });
            app.UseIdentityServer ();

            app.UseRouting ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}