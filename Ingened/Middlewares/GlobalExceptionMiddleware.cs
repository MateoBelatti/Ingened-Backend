using System.Net;
using System.Text.Json;

namespace Ingened.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ha ocurrido una excepción no controlada: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = _env.IsDevelopment() ? exception.Message : "Ha ocurrido un error interno en el servidor. Inténtelo más tarde.";

        switch (exception)
        {
            case Ingened.Exceptions.NotFoundException e:
                statusCode = (int)HttpStatusCode.NotFound;
                message = e.Message;
                break;
            case Ingened.Exceptions.BadRequestException e:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = e.Message;
                break;
            case Ingened.Exceptions.UnauthorizedException e:
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = e.Message;
                break;
        }

        context.Response.StatusCode = statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            Detail = _env.IsDevelopment() && statusCode == (int)HttpStatusCode.InternalServerError ? exception.StackTrace : null
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });
        
        return context.Response.WriteAsync(json);
    }
}
