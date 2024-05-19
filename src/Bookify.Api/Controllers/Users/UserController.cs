using Bookify.Application.User.GetLogInUser;
using Bookify.Application.User.LoginUser;
using Bookify.Application.User.RegisterUser;
using Bookify.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController(ISender sender) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost(nameof(Request))]
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

        [HttpGet(nameof(GetLoggedInUser))]
        [HasPermission(Permission.UsersRead)]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
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
            var command = new LoginUserCommand(request.Email, request.Password);

            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
