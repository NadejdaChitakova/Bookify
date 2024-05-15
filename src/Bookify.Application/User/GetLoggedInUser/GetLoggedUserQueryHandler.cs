using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.User.GetLogInUser;
using Bookify.Domain.Abstractions;
using Dapper;

namespace Bookify.Application.User.GetLoggedInUser
{
    internal sealed class GetLoggedUserQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContext userContext) : IQueryHandler<GetLoggedInUserQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            using var connection = sqlConnectionFactory.CreateConnection();

            const string sql = """
                               SELECT
                                   id AS Id,
                                   first_name AS FirstName,
                                   last_name AS LastName,
                                   email AS Email
                               FROM users
                               WHERE identity_id = @IdentityId
                               """;

            var user = await connection.QuerySingleAsync<UserResponse>(
                                                                       sql,
                                                                       new
                                                                       {
                                                                           userContext.IdentityId
                                                                       });

            return user;
        }
    }
}
