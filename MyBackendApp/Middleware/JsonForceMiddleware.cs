using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyBackendApp.Middleware
{
    public class JsonForceMiddleware
    {
        private readonly RequestDelegate _next;

        public JsonForceMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers["Accept"] = "application/json";
            await _next(context);
        }
    }
}
