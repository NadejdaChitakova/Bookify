using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public sealed class Apartment(Guid id,
    Name name,
        Description description,
    Address address,
    Money price,
    Money cleaningFee,
    DateTime? lastBookenOnUtc,
    List<Amenity> amenities) : Entity(id) // sealed to avoid inheritance and optimize performence
{
    public Name Name { get; private set; } = name;

    public Description Description { get; private set; } = description;

    public Address Address { get; private set; } = address;

    public Money Price { get; private set; } = price;

    public Money CleaningFee { get; private set; } = cleaningFee;

    public DateTime? LastBookedOnUtc { get; private set; } = lastBookenOnUtc;

    public List<Amenity> Amenities { get; private set; } = amenities;
}