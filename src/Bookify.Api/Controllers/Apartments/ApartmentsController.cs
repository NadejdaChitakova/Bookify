using Bookify.Application.Apartments.GetApartments;
using Bookify.Application.Apartments.Searchapartments;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Apartments
{
    //[Authorize]
    [ApiController]
    [Route("api/Apartments")]
    [EnableCors]
    public class ApartmentsController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet(nameof(GetApartments))]
        //[HasPermission(Permission.UsersRead)]
        public async Task<IActionResult> GetApartments(
            CancellationToken cancellationToken)
        {
            var query = new GetApartmentsQuery();

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet(nameof(SearchApartment))]
        public async Task<IActionResult> SearchApartment(
            DateOnly startDate,
            DateOnly endDate,
            CancellationToken cancellationToken)
        {
            var query = new SearchApartmentsQuery(startDate, endDate);

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
