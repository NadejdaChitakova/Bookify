using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments.Events;
using Bookify.Domain.Shared;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        DateTime? lastBookenOnUtc,
        List<Amenity> amenities) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        LastBookedOnUtc = lastBookenOnUtc;
        Amenities = amenities;
    }
private Apartment() { }

    public Name Name { get; private set; }

    public Description Description { get; private set; } 

    public Address Address { get; private set; } 

    public Money Price { get; private set; } 

    public Money CleaningFee { get; private set; } 

    public DateTime? LastBookedOnUtc { get; internal set; } 

    public List<Amenity> Amenities { get; private set; }

    public static Result<Apartment> Add(
        Name name,
Description description,
        Address address,
        Money price, 
        Money cleaningFee,
        List<Amenity> amenities)
    {
        var apartment = new Apartment(
                                      Guid.NewGuid(), 
                                      name,
                                      description,
                                      address,
                                      price,
                                      cleaningFee,
                                      null,
                                      amenities);

        apartment.RaiseDomainEvent(new AddApartmentDomainEvent(apartment.Id));

        return apartment;
    }
}