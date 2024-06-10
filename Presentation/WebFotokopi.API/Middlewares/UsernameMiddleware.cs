using Serilog.Context;

namespace WebFotokopi.API.Middlewares
{
    public class UsernameMiddleware
    {
        private readonly RequestDelegate next;

        public UsernameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            LogContext.PushProperty("Username", context.User.Identity.Name);
            return next(context);
        }
    }
}
