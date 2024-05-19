using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments.Events
{
    public sealed record AddApartmentDomainEvent(Guid id) : IDomainEvent;
}
