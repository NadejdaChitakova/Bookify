using System.Security.Claims;
using Bookify.Application.Abstractions.Authentication;
using Microsoft.AspNetCore.Http;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string IdentityId =>
            _httpContextAccessor
                .HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier).Value ??
            throw new ApplicationException("User context is unavailable");
    }
}
