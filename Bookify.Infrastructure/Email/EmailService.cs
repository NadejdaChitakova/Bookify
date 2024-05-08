using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Infrastructure.Email
{
    public sealed class EmailService : IEmailService
    {
        public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
return Task.CompletedTask;
        }
    }
}
