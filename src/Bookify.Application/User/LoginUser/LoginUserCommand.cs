using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.User.LoginUser
{
    public sealed class LoginUserCommand(string Email, string Password)
        : ICommand<AccessTokenResponse>;
}
