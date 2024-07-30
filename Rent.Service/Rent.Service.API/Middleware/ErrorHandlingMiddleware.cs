using Rent.Service.API.Dtos;
using Rent.Service.Application.Common.Exceptions;
using System.Net;

namespace Rent.Service.API.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (InvalidRequestException ex)
        {
            var problemDetails = GetBadRequestProblemDetails(ex);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsJsonAsync(problemDetails);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Resource not found");
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized access");
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "An unexpected error occurred");
        }
    }

    private ErrorDto GetBadRequestProblemDetails(InvalidRequestException ex)
    {
        string traceId = Guid.NewGuid().ToString();

        return new ErrorDto(ex, traceId);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string message)
    {
        var problemDetails = new
        {
            Status = (int)statusCode,
            Title = message,
            Detail = exception.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}
