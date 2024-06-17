using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Bookify.Application.Abstractions.Behaviour
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    where TResponse: Result
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
_logger.LogInformation("Executing command {Commands}", name);

var result = await next();

if (result.IsSuccess)
{
    _logger.LogInformation("Command {Command} processed successfully", name);
                }
else
{
    using (LogContext.PushProperty("Error", result.Error, true))
    {
        _logger.LogError("Command {Command} processed with error", name);
    }
}

return result;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Command {Command} processing failed", name);

                throw;
            }
        }
    }
}
