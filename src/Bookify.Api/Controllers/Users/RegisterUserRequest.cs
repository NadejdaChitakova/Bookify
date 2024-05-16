namespace Bookify.Api.Controllers.User
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password);
}