using ToDoListApp.Domain.Models.DTOs;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorDetails = new ErrorDetails
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = exception.Message,
            Details = exception.StackTrace 
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorDetails.StatusCode;

        return context.Response.WriteAsync(errorDetails.ToString());
    }

}
