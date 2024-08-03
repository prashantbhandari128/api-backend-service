using BackendService.Helper.Implementation;
using BackendService.Helper.Interface;
using System.Diagnostics;

namespace BackendService.Middlewares
{
    public class HttpLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConsoleHelper _consoleHelper;
        private readonly ILogger<HttpLogMiddleware> _logger;

        public HttpLogMiddleware(RequestDelegate next, IConsoleHelper consoleHelper, ILogger<HttpLogMiddleware> logger)
        {
            _next = next;
            _consoleHelper = consoleHelper;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                LogHttpInfo($"[+] Incoming Request: {context.Request.Method} {context.Request.Path}");

                // Call the next middleware in the pipeline
                await _next(context);

                LogHttpInfo($"[+] Outgoing Response: {context.Response.StatusCode}");
            }
            finally
            {
                stopwatch.Stop();
                LogHttpInfo($"[+] Request Processing Time: {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        private void LogHttpInfo(string message)
        {
            var formattedMessage = _consoleHelper.Decorate(ConsoleHelper.BOLD, ConsoleHelper.BACKGROUND_COLORS["black"], ConsoleHelper.COLORS["green"], message);
            _logger.LogInformation(formattedMessage);
        }
    }
}
