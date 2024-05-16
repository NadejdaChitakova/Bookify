using Bookify.Domain.Reviews;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class ReviewRepository(ApplicationDbContext context)
        : Repository<Review>(context), IReviewRepository;
}
