using ProjectVBack.Crosscutting.CustomExceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ProjectVBack.WebApi.Services.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private const string UnhandledMessage = "Fatal error";
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var message = UnhandledMessage;

                if (exception is AppIGetMoneyException)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = exception?.Message ?? $"Custom exception {nameof(exception)}";
                }

                var result = JsonSerializer.Serialize(new { message });
                await response.WriteAsync(result);

                LogException(exception);
            }
        }

        private void LogException(Exception? exception)
        {
            var logMessageBuilder = new StringBuilder();

            logMessageBuilder.AppendLine("\nException message:");
            logMessageBuilder.AppendLine(exception?.Message);
            logMessageBuilder.AppendLine("Stack:");
            logMessageBuilder.AppendLine(exception?.StackTrace);
            logMessageBuilder.AppendLine();

            _logger.LogError(logMessageBuilder.ToString());
        }
    }
}
