using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookify.Infrastructure
{
    public sealed class ApplicationDbContext(DbContextOptions options,
        IDateTimeProvider dateTimeProvider) : DbContext(options), IUnitOfWork
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                AddDomainEventsAsOutboxMessages();

                var result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException("Concurancy exception occured", e);
            }
        }

        private void AddDomainEventsAsOutboxMessages()
        {
            var outboxMessages = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvens = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvens;
                })
                .Select(domainEvent => new OutboxMessage(
                
                    Guid.NewGuid(),
                                                        dateTimeProvider.UtcNow,
                    domainEvent.GetType().Name,
                    JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)
                    ))
                .ToList();

AddRange(outboxMessages);
        }
    }
}
