using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace Megarender.WebAPIService.Middleware
{
    public class CountRequestMiddleware: IConventionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MetricReporter _reporter;

        public CountRequestMiddleware(RequestDelegate next, MetricReporter reporter)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {         
            Metrics.CreateCounter("PathCounter", "Count request", 
                                                new CounterConfiguration{
                                                    LabelNames = new [] {"method", "endpoint"}
                                                }).WithLabels(httpContext.Request.Method, httpContext.Request.Path).Inc();            
            await _next(httpContext);
            return;
        }
    }
}