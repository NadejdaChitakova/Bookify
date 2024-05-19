﻿
using Bookify.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bookify.Infrastructure.Authorization
{
    internal sealed class PermissionAuthorizationHandler(
        IServiceProvider serviceProvider) : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            PermissionRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true} )
            {
                return;
            }

            using var scope = serviceProvider.CreateScope();

            var authorizationService = scope.ServiceProvider.GetService<AuthorizationService>();

            var identityId = context.User.GetIdentityId();

            HashSet<string> permissions = await authorizationService.GetPermissionsForUser(identityId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
