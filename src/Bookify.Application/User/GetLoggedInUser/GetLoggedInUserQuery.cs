using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.User.GetLogInUser;

public sealed record GetLoggedInUserQuery() : IQuery<UserResponse>;