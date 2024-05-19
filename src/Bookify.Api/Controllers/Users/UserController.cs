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
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegisterUserCommand(
                                                  request.Email,
                                                  request.FirstName,
                                                  request.LastName,
                                                  request.Password);

            var result = await _sender.Send(command, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet]
        [HasPermission(Permission.UsersRead)]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(
            LogInUserRequest request,
        CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return Unauthorized(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
