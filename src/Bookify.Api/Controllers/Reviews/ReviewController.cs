﻿using Bookify.Application.Reviews.AddReview;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Reviews
{
    [Authorize]
    [ApiController]
    [Route("api/reviews")]
    public class ReviewController(ISender sender) : ControllerBase
    {
        [HttpPost(nameof(AddReview))]
        public async Task<IActionResult> AddReview(AddReviewRequest request, CancellationToken cancellationToken)
        {
            var command = new AddReviewCommand(request.BookingId, request.Rating, request.Comment);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}
