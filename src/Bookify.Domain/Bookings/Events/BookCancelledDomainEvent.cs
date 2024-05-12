using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events
{
    public sealed record BookCancelledDomainEvent(Guid id) : IDomainEvent;
}
