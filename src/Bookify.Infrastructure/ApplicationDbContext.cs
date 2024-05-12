﻿using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure
{
    public sealed class ApplicationDbContext(DbContextOptions options,
        IPublisher publisher) : DbContext(options), IUnitOfWork
    {
        private readonly IPublisher _publisher = publisher;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventsAsync();

                return result;
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException("Concurancy exception occured", e);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvens = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvens;
                });

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}