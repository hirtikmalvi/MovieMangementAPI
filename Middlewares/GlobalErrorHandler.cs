using MovieManagementAPI.Utilities;
using System.Net;
using System.Text.Json;

namespace MovieManagementAPI.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly ILogger<GlobalErrorHandler> _logger;
        private readonly RequestDelegate _next;

        public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception occurred: {ex.Message}");
                _logger.LogError(ex, $"InnerException occurred: {ex?.InnerException.Message}");

                var response = CustomResult<string>.Fail(500, "An unexpected error occurred.", [
                    $"Message: {ex?.Message}",
                    $"InnerException Message: {ex?.InnerException?.Message}"
                    ]
                );

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 500;

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }
    }
}
