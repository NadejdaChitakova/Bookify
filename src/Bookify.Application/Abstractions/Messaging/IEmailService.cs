using Bookify.Domain.Users;

namespace Bookify.Application.Abstractions.Messaging
{
    public interface IEmailService
    {
        Task SendAsync(Email recipient, string subject, string body);
    }
}
