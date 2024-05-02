using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Apartments.SearchAppartments;

public record SearchApartmentsQuery(
    DateOnly dateStart,
    DateOnly dateEnd): IQuery<IReadOnlyList<ApartmentResponse>>;