using Bookify.Application.Caching;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bookify.Application.Booking.GetBooking
{
    internal sealed class QueryCachingBehaviour<TRequest, TResponse>(
        ICacheService cacheService,
        ILogger<QueryCachingBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : Result
    {

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            TResponse? cachedResult = await cacheService.GetAsync<TResponse>(
                                                                             request.CachedKey,
                                                                             cancellationToken);

            string name = typeof(TRequest).Name;

            if (cachedResult is not null)
            {
                logger.LogInformation("Cached hit for {Query}", name);

                return cachedResult;
            }

            logger.LogInformation("Cached miss for {Query}", name);

            var result = await next();

            if (result.IsSuccess)
            {
                await cacheService.SetAsync(request.CachedKey, result, request.Expiration, cancellationToken);
            }

            return result;
        }
    }
}
