namespace KampusSggwBackend.Configuration.Middlewares;

using KampusSggwBackend.Domain.Exceptions;
using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    // Fields
    private readonly RequestDelegate _next;

    // Constructor
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Methods
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException appException)
        {
            await HandleException(context, appException);
        }
    }

    private static async Task HandleException(HttpContext context, AppException exception)
    {
        context.Response.StatusCode = (int)(exception switch
        {
            ResourceValidationException _ => HttpStatusCode.BadRequest,
            AuthException _ => HttpStatusCode.Unauthorized,
            ResourceNotFoundException _ => HttpStatusCode.NotFound,
            AccessDeniedException _ => HttpStatusCode.Forbidden,
            _ => throw new ArgumentOutOfRangeException()
        });

        ErrorResponse errorResponse = exception switch
        {
            ResourceValidationException =>
                new ErrorResponse()
                {
                    Code = "InvalidResource",
                    Description = "One or more validation errors occurred.",
                    AdditionalInformation = "",
                },
            AuthException _ => new ErrorResponse()
                {
                    Code = "InvalidResource",
                    Description = "One or more validation errors occurred.",
                    AdditionalInformation = "",
                },
            //ResourceNotFoundException _ => HttpStatusCode.NotFound,
            //AccessDeniedException _ => HttpStatusCode.Forbidden,
            _ => throw new ArgumentOutOfRangeException()
        };

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var payload = JsonSerializer.Serialize(errorResponse, serializerOptions);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(payload);
    }
}
