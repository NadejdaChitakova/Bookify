using System.Net.Http.Json;
using Bookify.Application.Abstractions.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;

namespace Bookify.Infrastructure.Authentication
{
    internal sealed class AuthenticationService(HttpClient httpClient) : IAuthenticationService
    {
        private const string PasswordCredentialType = "password";

        public async Task<string> RegisterAsync(User user,
            string password, 
            CancellationToken cancellationToken = default)
        {
            var userRepresentationModel = UserRepresentationModel.FromUser(user);

            userRepresentationModel.Credentials = new CredentialRepresentationModel[]
            {
                new()
                {
                    Value = password,
                    Temporary = false,
                    Type = PasswordCredentialType
                }
            };

            var response = await httpClient.PostAsJsonAsync(
                                                             "users",
                                                             userRepresentationModel,
                                                             cancellationToken);

            return ExtractIdentityFromLocationHeader(response);
        }

        private static string ExtractIdentityFromLocationHeader(HttpResponseMessage httpResponseMessage)
        {
            const string userSegmentName = "users/";

            var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;

            if (locationHeader is null)
            {
                throw new InvalidOperationException("Location header cannot be null");
            }

            var userSegmentValueIndex = locationHeader.IndexOf(
                                                               userSegmentName,
                                                               StringComparison.InvariantCultureIgnoreCase);

            var userIdentityId = locationHeader.Substring(
                                                          userSegmentValueIndex + userSegmentName.Length);

            return userIdentityId;
        }
    }
}
