using MediatR;
using Microsoft.Extensions.Logging;

namespace Rent.Service.Application.Common.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    (ILogger<IPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Start request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        logger.LogInformation(
            "Completed request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        logger.LogInformation(
            "Response request {@RequestName}, {@Response}",
            typeof(TRequest).Name,
            result);

        return result;
    }
}
