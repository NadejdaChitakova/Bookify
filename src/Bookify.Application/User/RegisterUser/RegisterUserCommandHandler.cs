using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.User.RegisterUser
{
    internal sealed class RegisterUserCommandHandler(
        IAuthenticationService authenticationService,
        IUserRepository userReposity,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RegisterUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = Domain.Users.User.Create(
                                                new FirstName(request.FirstName),
                                                new LastName(request.LastName),
                                                new Email(request.Email));

            var identityId = await authenticationService.RegisterAsync(
                                                                        user,
                                                                        request.Password,
                                                                        cancellationToken);

            user.SetIdentityId(identityId);

userReposity.Add(user);

await unitOfWork.SaveChangesAsync(cancellationToken);

return user.Id;
        }
    }
}
