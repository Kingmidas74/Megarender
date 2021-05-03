using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Megarender.WebServiceCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Megarender.WebServiceCore
{
    public static class IServiceCollectionExtensions {
        
        public static IServiceCollection AddAuth (this IServiceCollection services, string connectionString) {            
            var identityServerURI = string.Format (connectionString, 
                            Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.PIS_HOST)), 
                            Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.PIS_PORT)));
            services.AddAuthentication ("Bearer")
                .AddJwtBearer ("Bearer", options => {
                    options.Authority = identityServerURI;
                    options.RequireHttpsMetadata = false;                    
                    options.TokenValidationParameters.ValidateAudience = false;
                });
            return services;
        }
        public static IServiceCollection AddSwagger<T> (this IServiceCollection services, string connectionString) 
        {
            var identityServerUri = string.Format (connectionString, 
                            Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.PIS_HOST_EXT)), 
                            Environment.GetEnvironmentVariable (nameof (EnvironmentVariables.PIS_PORT_EXT)));

            services.AddApiVersioning(options => {  
                options.ReportApiVersions=true;
                options.AssumeDefaultVersionWhenUnspecified=false;
                options.DefaultApiVersion = new ApiVersion(0,1);
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
                    c.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {  
                        Title = $"{typeof(T).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product}",  
                        Version = description.ApiVersion.ToString(),  
                        Description = description.IsDeprecated ? $"{typeof(T).GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : typeof(T).Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,  
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
                            AuthorizationUrl = new Uri($"{identityServerUri}connect/authorize"),
                            TokenUrl = new Uri($"{identityServerUri}connect/token"),
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
                        new[] { "megarender_api"}
                    }
                });
                
                var xmlFile = $"{typeof(T).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments (xmlPath);
            });
            return services;
        }
    }
}