using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aden.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = request.GetType();
        logger.LogInformation("{Request} is starting.", requestName);
        var timer = Stopwatch.StartNew();
        var response = await next();
        timer.Stop();
        logger.LogInformation("{Request} has finished in {Time}ms.", requestName, timer.ElapsedMilliseconds);
        return response;
    }
}