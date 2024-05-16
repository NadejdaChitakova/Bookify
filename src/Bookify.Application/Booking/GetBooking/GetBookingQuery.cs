using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Booking.GetBooking;

public record GetBookingQuery(Guid BookingId) : IQuery<BookingResponse>;