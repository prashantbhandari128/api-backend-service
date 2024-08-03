using BackendService.Middlewares;

namespace BackendService.Extensions
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseHttpLog(this IApplicationBuilder builder) => builder.UseMiddleware<HttpLogMiddleware>();

        public static IApplicationBuilder UseXssValidation(this IApplicationBuilder builder) => builder.UseMiddleware<XssValidationMiddleware>();
    }
}
