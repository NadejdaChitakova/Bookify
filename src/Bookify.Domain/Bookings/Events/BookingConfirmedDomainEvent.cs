﻿using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings.Events
{
    public sealed record BookingConfirmedDomainEvent(Guid id) : IDomainEvent;
}