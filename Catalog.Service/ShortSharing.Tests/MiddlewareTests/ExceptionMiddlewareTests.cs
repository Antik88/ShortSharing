using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShortSharing.API.Middlewares;
using ShortSharing.BLL.Common.Exceptions;
using System.Text.Json;
using Xunit;

namespace ShortSharing.Tests.MiddlewareTests;

public class ExceptionMiddlewareTests
{
    private readonly Mock<RequestDelegate> _next;
    private readonly ExceptionMiddleware _middleware;

    public ExceptionMiddlewareTests()
    {
        _next = new Mock<RequestDelegate>();
        _middleware = new ExceptionMiddleware(_next.Object);
    }


    [Fact]
    public async Task InvokeAsync_ThrowsBadRequestException_ReturnsBadRequestResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var exception = new BadRequestException("Bad request");
        _next.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);

        // Act
        await _middleware.InvokeAsync(context);

        // Assert

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(context.Response.Body);
        var response = await reader.ReadToEndAsync();

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response);

        Assert.NotNull(problemDetails);
        Assert.Equal("Bad request", problemDetails.Detail);
    }

    [Fact]
    public async Task InvokeAsync_ThrowsForbiddenException_ReturnsForbiddenResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exception = new ForbiddenException("Access denied");
        _next.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);

        // Act
        await _middleware.InvokeAsync(context);

        // Assert
        var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response);

        Assert.Equal("Access denied", problemDetails.Detail);
    }

    [Fact]
    public async Task InvokeAsync_ThrowsGeneralException_ReturnsInternalServerErrorResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var exception = new Exception("An error occurred");
        _next.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);

        // Act
        await _middleware.InvokeAsync(context);

        // Assert
        var response = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response);

        Assert.Equal("An error occurred", problemDetails.Detail);
    }
}