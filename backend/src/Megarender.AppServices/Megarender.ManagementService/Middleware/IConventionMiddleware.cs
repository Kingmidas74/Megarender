using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Megarender.ManagementService.Middleware {    
    public interface IConventionMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}