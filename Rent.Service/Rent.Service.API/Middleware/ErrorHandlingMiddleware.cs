using Microsoft.AspNetCore.Mvc;
using Rent.Service.API.Dtos;
using Rent.Service.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace Rent.Service.API.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(InvalidRequestException ex)
        {
            var problemDetails = GetBadRequestProblemDetails(ex); 

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsJsonAsync(problemDetails);
        }
    }

    private ErrorDto GetBadRequestProblemDetails(InvalidRequestException ex)
    {
        string traceId = Guid.NewGuid().ToString();

        return new ErrorDto(ex, traceId);
    }
}
