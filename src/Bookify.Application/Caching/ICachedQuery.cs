using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Caching
{
    public interface ICachedQuery
    {
        string CachedKey { get; }

        TimeSpan? Expiration { get; }
    }

    public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;
}
