﻿using System.Diagnostics;

namespace ShortSharing.API.Middlewares;

public class LoggingMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<LoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context) 
    {
        var timer = new Stopwatch();
        timer.Start();

        try
        {
            await _next(context);
        }
        finally 
        {
            timer.Stop();
            LogRequest(context, timer.Elapsed);
        }
    }

    private void LogRequest(HttpContext context, TimeSpan duration)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var statusCode = context.Response.StatusCode;

        _logger.LogInformation(
            "HTTP {@Method} {@Path} responded with {@StatusCode} in {@TotalMilliseconds}ms",
            method, path, statusCode, duration.TotalMilliseconds
        );
    }
}
