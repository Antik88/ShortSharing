using Microsoft.AspNetCore.Mvc;
using Rent.Service.Application.Common.Exceptions;
using System.Net;

namespace Rent.Service.API.Dtos;

public class ErrorDto : ProblemDetails
{
    public List<string> Errors { get; }

    public ErrorDto(InvalidRequestException exception, string traceId)
    {
        Title = "Request validation error";
        Status = (int)HttpStatusCode.BadRequest;
        Errors = exception.Errors;
        Instance = traceId;
    }
}
