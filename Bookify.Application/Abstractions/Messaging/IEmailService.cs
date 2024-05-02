namespace Bookify.Application.Abstractions.Messaging
{
    public interface IEmailService
    {
        Task SendAsync(Domain.User.Email  recipient, string subject, string body);
    }
}
