using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Megarender.WebAPIService.Middleware {    
    public interface IConventionMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}