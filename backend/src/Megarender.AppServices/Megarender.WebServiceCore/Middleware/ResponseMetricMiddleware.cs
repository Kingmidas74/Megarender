using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Megarender.WebServiceCore.Middleware
{
    public class ResponseMetricMiddleware: IConventionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly MetricReporter _reporter;

        public ResponseMetricMiddleware(RequestDelegate request, MetricReporter reporter)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
            _reporter = reporter ?? throw new ArgumentNullException(nameof(reporter));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                await _request.Invoke(httpContext);
            }
            finally
            {
                sw.Stop();
                _reporter.RegisterRequest();
                _reporter.RegisterResponseTime(httpContext.Response.StatusCode, httpContext.Request.Method, sw.Elapsed);
            }
        }
    }
}