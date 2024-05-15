using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
        public override void Add(User entity)
        {
            foreach (var role in entity.Roles)
            {
                DbContext.Attach(role);
            }

base.Add(entity);
        }
    }
}
