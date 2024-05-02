using Bookify.Domain.Abstractions;

namespace Bookify.Application.Abstractions
{
    public sealed class ApartmentErrors
    {
        public static Error NotFound = new(
                                           "Apartment.NotFound",
                                           "The apartment with the specified identifier was not found");
    }
}
