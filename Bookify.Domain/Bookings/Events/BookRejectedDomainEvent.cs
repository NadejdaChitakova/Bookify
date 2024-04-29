using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events
{
    public sealed class BookRejectedDomainEvent(Guid id) : IDomainEvent;
}