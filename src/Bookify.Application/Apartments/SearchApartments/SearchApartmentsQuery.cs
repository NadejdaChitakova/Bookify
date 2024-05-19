using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Apartments.SearchApartments;

namespace Bookify.Application.Apartments.Searchapartments;

public record SearchApartmentsQuery(
    DateOnly dateStart,
    DateOnly dateEnd): IQuery<IReadOnlyList<ApartmentResponse>>;