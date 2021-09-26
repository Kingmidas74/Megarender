using System;
using System.Collections.Generic;
using System.Text;
using Megarender.Features.Exceptions;
using Megarender.DataAccess.Extensions;
using Megarender.WebServiceCore.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Megarender.WebServiceCore
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x=> {
                x.Run(async context => {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;
                    (string content, int code) = exception switch {
                        BusinessException e when exception is BusinessException => (JsonConvert.SerializeObject(e.Properties), StatusCodes.Status400BadRequest),
                        _ => (JsonConvert.SerializeObject(new Dictionary<string,object> {
                                {"Processing error","Contact to tech support"}
                            }), StatusCodes.Status500InternalServerError)
                    };
                    context.Response.StatusCode=code;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(content, Encoding.UTF8);
                });
            });
        }

        public static void ApplyMigrations<T>(this IApplicationBuilder app) where T:DbContext
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory> ().CreateScope ()) 
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<T> ();                    
                    
                if(!context.AllMigrationsApplied())
                {
                    context.Database.Migrate();
                }                
            }
        }

        public static void UseMiddlewareByPath<T>(this IApplicationBuilder app, PathString path) where T:IConventionMiddleware
        {
            Func<HttpContext, bool> predicate = c =>
            {

                var result =  path.HasValue && 
                        (c.Request.Path.StartsWithSegments(path, out var remaining) 
                        && !string.IsNullOrEmpty(remaining));
                return result;
            };

            app.UseWhen(
                predicate, 
                b => 
                {
                    b.UseMiddleware<T> ();
                }
            );
        }

        public static void UseAPIMiddlewares(this IApplicationBuilder app)
        {                        
            app.UseMiddlewareByPath<RequestResponseLoggingMiddleware>("/api");
            app.UseMiddlewareByPath<CountRequestMiddleware>("/api");
            app.UseMiddlewareByPath<ResponseMetricMiddleware>("/api");
        }
    }
}