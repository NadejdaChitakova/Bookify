using Bookify.Application.Booking.GetBooking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings
{
    [ApiController]
    [Route("api/Apartments")]
    public class BookingsController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet]
        public async Task<ActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBookingQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
