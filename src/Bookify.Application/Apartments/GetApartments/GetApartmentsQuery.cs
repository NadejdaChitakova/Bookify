using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Apartments.GetApartments
{
    public record GetApartmentsQuery() : IQuery<IReadOnlyList<ApartmentResponse>> { }
}
