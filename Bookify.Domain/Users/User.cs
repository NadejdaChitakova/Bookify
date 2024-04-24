using Bookify.Domain.Abstractions;

namespace Bookify.Domain.User;

public class User : Entity
{
    public User(Guid id) : base(id)
    {
    }
public FirstName FirstName { get; private set; }

public LastName LastName { get; private set; }

public Email Email { get; private set; }

}