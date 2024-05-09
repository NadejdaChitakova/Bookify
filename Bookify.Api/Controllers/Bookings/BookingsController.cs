using Bookify.Application.Booking.GetBooking;
using Bookify.Application.Booking.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings
{
    [ApiController]
    [Route("api/Bookings")]
    public class BookingsController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet]
        public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        public async Task<IActionResult> ReserveBooking(
            ReserveBookingRequest request, 
            CancellationToken cancellationToken)
        {
            var command = new ReserveBookingCommand(
                                                   request.ApartmentId,
                                                   request.UserId,
                                                   request.StartDate,
                                                   request.EndDate);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(GetBooking), new { id = result.Value }, result.Value);
        }
    }
}
