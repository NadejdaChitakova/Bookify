using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Caching;

namespace Bookify.Application.Booking.GetBooking;

public record GetBookingQuery(Guid BookingId) : ICachedQuery<BookingResponse>
{
    public string CachedKey => $"bookings-{BookingId}";

    public TimeSpan? Expiration => null;
}