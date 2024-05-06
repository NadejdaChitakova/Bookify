using Bookify.Domain.User;

namespace Bookify.Application.Abstractions.Messaging
{
    public interface IEmailService
    {
        Task SendAsync(Email  recipient, string subject, string body);
    }
}
