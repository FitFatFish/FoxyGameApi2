using Foxy.WebApi.Middlewares;
using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception");

            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = ex switch
            {
                BadHttpRequestException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var result = new
            {
                status = (int)statusCode,
                message = statusCode switch
                {
                    HttpStatusCode.BadRequest => "درخواست نامعتبر",
                    HttpStatusCode.Unauthorized => "دسترسی غیرمجاز",
                    HttpStatusCode.NotFound => "یافت نشد",
                    _ => "خطای داخلی سرور"
                },
                detail = _env.IsDevelopment() ? ex.Message : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await response.WriteAsync(JsonSerializer.Serialize(result, options));
        }
    }
}
