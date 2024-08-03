using BackendService.Helper.Implementation;
using BackendService.Helper.Interface;
using BackendService.Models.Response.Template.Process;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BackendService.Middlewares
{
    public class XssValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConsoleHelper _consoleHelper;
        private readonly ILogger<XssValidationMiddleware> _logger;

        public XssValidationMiddleware(RequestDelegate next, IConsoleHelper consoleHelper, ILogger<XssValidationMiddleware> logger)
        {
            _next = next;
            _consoleHelper = consoleHelper;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Validate the request
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                context.Request.EnableBuffering();
                var bodyAsText = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (ContainsXss(bodyAsText))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest; // 400 Bad Request
                    context.Response.ContentType = "application/json";
                    var response = new ProcessResult().SetResult(false, $"Bad Request: Detected potential XSS content.");
                    var jsonResponse = JsonConvert.SerializeObject(response);
                    context.Response.ContentLength = jsonResponse.Length;
                    await context.Response.WriteAsync(jsonResponse);
                    LogXssWarning("[+] Potential XSS content detected.");
                    return;
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private static bool ContainsXss(string input)
        {
            // Simple XSS detection using regular expressions
            // This can be enhanced with more sophisticated checks
            var xssPattern = new Regex(@"<.*script.*>", RegexOptions.IgnoreCase);
            return xssPattern.IsMatch(input);
        }

        private void LogXssWarning(string message)
        {
            var formattedMessage = _consoleHelper.Decorate(ConsoleHelper.BOLD, ConsoleHelper.BACKGROUND_COLORS["black"], ConsoleHelper.COLORS["red"], message);
            _logger.LogWarning(formattedMessage);
        }
    }
}
