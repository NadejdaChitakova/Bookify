using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public sealed class Apartment(Guid id) : Entity(id) // sealed to avoid inheritance and optimize performence
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public Address Address { get; private set; }

    public decimal PrimeAmount { get; private set; }

    public string PrimeCurrency { get; private set; }
    
    public decimal CleaningFeeAmount { get; private set; }

    public string CleaningFeeCurrency { get; private set; }

    public DateTime? LastBookedOnUtc { get; private set; }

    public List<Amenity> Amenities { get; private set; } = new();
}