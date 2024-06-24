using Asp.Versioning;
using Bookify.Application.User.GetLogInUser;
using Bookify.Application.User.LoginUser;
using Bookify.Application.User.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.User
{
    [ApiController]
    [ApiVersion(ApiVersions.V1)]
    [ApiVersion(ApiVersions.V2)]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController(ISender sender) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                                                  request.Email,
                                                  request.FirstName,
                                                  request.LastName,
                                                  request.Password);

            var result = await sender.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet(nameof(GetLoggedInUserV1))]
        [MapToApiVersion(ApiVersions.V1)]
        public async Task<IActionResult> GetLoggedInUserV1(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            var result = await sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet(nameof(GetLoggedInUserv2))]
        [MapToApiVersion(ApiVersions.V2)]
        public async Task<IActionResult> GetLoggedInUserv2(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            var result = await sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet(nameof(LogIn))]
        public async Task<IActionResult> LogIn(
            LogInUserRequest request,
        CancellationToken cancellationToken)
        {
            var command = new LogInUserCommand(request.Email, request.Password);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
