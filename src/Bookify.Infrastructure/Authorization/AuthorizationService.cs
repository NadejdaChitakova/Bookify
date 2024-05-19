using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization
{
    internal sealed class AuthorizationService(
        ApplicationDbContext dbContext)
    {
public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            var roles = await dbContext.Set<User>()
                            .Where(user => user.IdentityId == identityId)
                            .Select(user => new UserRolesResponse
                            {
Id = user.Id,
Roles = user.Roles.ToList(),
                            })
                            .FirstAsync();

            return roles;
        }

        public async Task<HashSet<string>> GetPermissionsForUser(string identityId)
        {
            var permission = await dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .SelectMany(user => user.Roles.Select(role => role.Permissions))
                .FirstAsync();

            var permissionsSet = permission.Select(x => x.Name).ToHashSet();

            return permissionsSet;
        }
    }
}
