using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.User.LoginUser
{
    internal sealed class LoginUserCommandHandler(
        IJwtService jwtService) : ICommandHandler<LoginUserCommand, AccessTokenResponse>
    {
        public Task<Result<AccessTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
