using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events
{
    public sealed class BookCompleteDomainEvent(Guid id) : IDomainEvent;
}