using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Megarender.DataStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Megarender.WebAPIService.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;
        
        public CachedAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisSettings>();
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheDataStorage>();
            var cacheKey = this.GenerateChacheKeyFromRequest(context.HttpContext.Request);

            if(cacheSettings.Enabled) 
            {   
                var cachedValue = await cacheService.RetriveDataAsync<object>(cacheKey, context.HttpContext.RequestAborted);

                if (cachedValue is not null)
                {
                    var contentResult = new ContentResult
                    {
                        Content = JsonSerializer.Serialize(cachedValue),
                        ContentType = "application/json",
                        StatusCode = 200,
                    };
                    context.Result = contentResult;
                    return;
                }
            }

            var executedContext = await next();
            if (cacheSettings.Enabled && executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheDataAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveInSeconds), context.HttpContext.RequestAborted);
            }
        }

        private string GenerateChacheKeyFromRequest(HttpRequest request) 
        {
            var result = new StringBuilder(request.Path);
            foreach(var (key, value) in request.Query.OrderBy(x=>x.Key))
            {
                result.Append($"|{key}|{value}");
            }
            return result.ToString();
        }
    }
}