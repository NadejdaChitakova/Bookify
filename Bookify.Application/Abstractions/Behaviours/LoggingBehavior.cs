using Bookify.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;
using System;

namespace Bookify.Application.Abstractions.Behaviour
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly ILogger<TRequest> _logger = logger;

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var name = request.GetType().Name;

            try
            {
_logger.LogInformation("Executing command {Commands}", name);

var result = await next();

_logger.LogInformation("Command {Command} processed successfully", name);

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
