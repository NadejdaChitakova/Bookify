using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.User.LoginUser
{
    internal sealed class LoginUserCommandHandler(
        IJwtService jwtService) : ICommandHandler<LogInUserCommand, AccessTokenResponse>
    {
        public async Task<Result<AccessTokenResponse>> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var result = await jwtService.GetAccessToken(
                                                         request.Email,
                                                         request.Password,
                                                         cancellationToken);

            return result.IsFailure 
                       ? Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials) 
                       : new AccessTokenResponse(result.Value);
        }
    }
}
