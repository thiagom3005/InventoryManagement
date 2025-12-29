using System.Net;
using System.Text.Json;
using InventoryManagement.Domain.Exceptions;

namespace InventoryManagement.API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundException notFoundException:
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { error = notFoundException.Message, type = "NotFound" });
                break;
            case ConflictException conflictException:
                code = HttpStatusCode.Conflict; // 409
                result = JsonSerializer.Serialize(new { error = conflictException.Message, type = "Conflict" });
                break;
            case BusinessRuleException businessRuleException:
                code = HttpStatusCode.UnprocessableEntity; // 422
                result = JsonSerializer.Serialize(new { error = businessRuleException.Message, type = "BusinessRule" });
                break;
            case DomainException domainException:
                code = HttpStatusCode.BadRequest; // 400
                result = JsonSerializer.Serialize(new { error = domainException.Message, type = "Validation" });
                break;
            default:
                code = HttpStatusCode.InternalServerError; // 500
                result = JsonSerializer.Serialize(new { error = "Ocorreu um erro interno no servidor", type = "Internal" });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
