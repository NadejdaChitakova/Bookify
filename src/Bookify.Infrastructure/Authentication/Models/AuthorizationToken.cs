using System.Text.Json.Serialization;

namespace Bookify.Infrastructure.Authentication.Models
{
    internal class AuthorizationToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = string.Empty;
    }
}
