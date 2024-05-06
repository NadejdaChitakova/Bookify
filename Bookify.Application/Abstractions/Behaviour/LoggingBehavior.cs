using Bookify.Application.Abstractions.Messaging;
using MediatR;

namespace Bookify.Application.Abstractions.Behaviour
{
    public class LoggingBehavior<TRequest, TResponse>
    :IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
