using FluentValidation;

namespace Bookify.Application.Booking.ReserveBooking
{
    public class ReservedBookingCommandValidator : AbstractValidator<ReserveBookingCommand>
    {
        public ReservedBookingCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();

            RuleFor(c => c.AparmentId).NotEmpty();

            RuleFor(c => c.StartDate).LessThan(c=> c.EndDate);
        }
    }
}
