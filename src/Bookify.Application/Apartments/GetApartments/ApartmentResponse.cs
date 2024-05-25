

namespace Bookify.Application.Apartments.GetApartments
{
    public sealed class ApartmentResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string Currency { get; init; }

        public AddressResponse Address { get; set; }

        public List<Image> Images { get; set; }
    }

    public sealed class Image
    {
        public Guid ApartmentPhotoId { get; init; }

        public bool IsMainPhoto { get; init; }

        public string ApartmentPhoto { get; init; }
    }
}
