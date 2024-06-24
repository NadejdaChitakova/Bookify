using System.Data;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Abstractions;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Quartz;

namespace Bookify.Infrastructure.Outbox
{
    [DisallowConcurrentExecution]
    internal sealed class ProcessOutboxMessagesJob(
        ISqlConnectionFactory connectionFactory,
        IPublisher publisher,
        IDateTimeProvider dateTimeProvider,
        OutboxOptions outboxOptions,
        ILogger<ProcessOutboxMessagesJob> logger) :IJob
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new()
        {
TypeNameHandling = TypeNameHandling.All
        };

        public async Task Execute(IJobExecutionContext context)
        {
logger.LogInformation("Beginning to process outbox messages");

using var connection = connectionFactory.CreateConnection();
using var transaction = connection.BeginTransaction();

var outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

foreach (var outboxMessage in outboxMessages)
{
    Exception? exception = null;

    try
    {
        var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                                                                      outboxMessage.Content,
                                                                      JsonSerializerSettings);

        await publisher.Publish(domainEvent, context.CancellationToken);
    }
    catch (Exception e)
    {
        logger.LogError(e,
                        "Exception while processing outbox message {messageId}",
                        outboxMessage.id);
    }

    await UpdateOutboxMessageAsync(connection, transaction, outboxMessage, exception);
}
transaction.Commit();

logger.LogInformation("Completed processing outbox messages");
        }

        private async Task UpdateOutboxMessageAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            OutboxMessageResponse outboxMessage,
            Exception? exception)
        {
            const string sql = @"Update outbox_messages
                    SET processed_on_utc = @ProcessedOnUtc,
                    error = @Error
                    where id = @id";

            await connection.ExecuteAsync(sql,
                                          new
                                          {
                                              outboxMessage.id,
                                              ProcessedOnUtc = dateTimeProvider.UtcNow,
                                              Error = exception?.ToString(),
                                              transaction
                                          });
        }

        private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
            IDbConnection connection,
            IDbTransaction transaction)
        {
var sql =$"""
                        SELECT id, content
                        FROM outbox_messages
                        WHERE processed_on_utc is NULL
                        ORDER BY occurred_on_utc
                        LIMIT {outboxOptions.BatchSize}
                        FOR UPDATE
                     """;
var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(sql, transaction: transaction);

return outboxMessages.ToList();
        }

        internal sealed record OutboxMessageResponse(Guid id, string Content);
    }
}
