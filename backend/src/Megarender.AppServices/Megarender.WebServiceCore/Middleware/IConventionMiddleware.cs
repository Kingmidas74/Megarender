using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Megarender.WebServiceCore.Middleware {    
    public interface IConventionMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}