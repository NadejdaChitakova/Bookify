﻿using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Booking.ReserveBooking;

public record ReserveBookingCommand(
    Guid aparmentId,
    Guid userId,
        DateOnly StartDate,
    DateOnly EndDate) : ICommand<Guid>; // i command represend the guid of created object