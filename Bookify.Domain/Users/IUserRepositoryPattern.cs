namespace Bookify.Domain.User;

public interface IUserRepositoryPattern
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

    void Add(User user);
}