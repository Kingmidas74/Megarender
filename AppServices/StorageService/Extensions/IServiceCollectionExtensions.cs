using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Megarender.StorageService.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Megarender.StorageService.DAL;

namespace Megarender.StorageService {

    public static class ServiceCollectionExtensions {

        public static IServiceCollection AddSQL (this IServiceCollection services, string connectionString) {            
            services.AddDbContextPool<StorageDbContext> ((provider, options) => {          
                
                if(!File.Exists(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_USER_FILE)))) throw new FileNotFoundException(nameof (EnvironmentVariables.DB_USER_FILE));
                if(!File.Exists(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PWD_FILE)))) throw new FileNotFoundException(nameof (EnvironmentVariables.DB_PWD_FILE));                
                
                options.UseNpgsql (
                    string.Format (connectionString, 
                                    System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_HOST)), 
                                    System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PORT)), 
                                    File.ReadAllText(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_USER_FILE))), 
                                    File.ReadAllText(System.Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.DB_PWD_FILE)))), 
                    providerOptions => {
                                providerOptions.EnableRetryOnFailure (3);                                
                    });
                var extension = options.Options.FindExtension<CoreOptionsExtension> ();
                if (extension != null) {
                    var loggerFactory = extension.ApplicationServiceProvider.GetService<ILoggerFactory> ();
                    if (loggerFactory != null) {
#if DEBUG
                        options.EnableSensitiveDataLogging ().UseLoggerFactory (loggerFactory);
#else
                        options.UseLoggerFactory (loggerFactory);
#endif
                    }
                };
            });
            return services;
        }

        public static IServiceCollection AddAuth (this IServiceCollection services, string connectionString) {            
            var identityServerURI = string.Format (connectionString, 
                            System.Environment.GetEnvironmentVariable (nameof (Models.EnvironmentVariables.PIS_HOST)), 
                            System.Environment.GetEnvironmentVariable (nameof (Models.EnvironmentVariables.PIS_PORT)));
            services.AddAuthentication ("Bearer")
                .AddJwtBearer ("Bearer", options => {
                    options.Authority = identityServerURI;
                    options.RequireHttpsMetadata = false;                    
                    options.TokenValidationParameters.ValidateAudience = false;
                });
            return services;
        }
        public static IServiceCollection AddSwagger (this IServiceCollection services, string connectionString) 
        {
            var identityServerURI = string.Format (connectionString, 
                            System.Environment.GetEnvironmentVariable (nameof (Models.EnvironmentVariables.PIS_HOST_EXT)), 
                            System.Environment.GetEnvironmentVariable (nameof (Models.EnvironmentVariables.PIS_PORT_EXT)));

            services.AddApiVersioning(options => {
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=false;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(0,1);
                options.ApiVersionReader = new HeaderApiVersionReader("X-Accept-Version");
            });
            services.AddVersionedApiExplorer(options => {  
                options.GroupNameFormat ="VVV";  
                options.SubstituteApiVersionInUrl = true;  
            });
            services.AddSwaggerGen (c => {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();  
  
                foreach (var description in provider.ApiVersionDescriptions)  
                {  
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo()  
                    {  
                        Title = $"{typeof(Startup).Assembly.GetCustomAttribute<System.Reflection.AssemblyProductAttribute>().Product}",  
                        Version = description.ApiVersion.ToString(),  
                        Description = description.IsDeprecated ? $"{typeof(Startup).GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : typeof(Startup).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,  
                        TermsOfService = new Uri ("https://example.com/terms"),
                        Contact = new OpenApiContact {
                            Name = "Suleymanov Denis",
                                Email = string.Empty,
                                Url = new Uri ("https://vk.com/iammidas"),
                        },
                        License = new OpenApiLicense {
                            Name = "Use under MIT",
                                Url = new Uri ("https://example.com/license"),
                        }                        
                    });  
                }  

                c.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme {

                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{identityServerURI}connect/authorize"),
                            TokenUrl = new Uri($"{identityServerURI}connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "megarender_api", "Full access " }
                            }                           
                        }                        
                    }
                });

                c.AddSecurityRequirement (new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] { "megarender_api"}
                    }
                });
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });
            return services;
        }
    }
}