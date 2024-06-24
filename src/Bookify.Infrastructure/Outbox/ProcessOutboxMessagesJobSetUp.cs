using Microsoft.Extensions.Options;
using Quartz;

namespace Bookify.Infrastructure.Outbox
{
    internal class ProcessOutboxMessagesJobSetUp(
        IOptions<OutboxOptions> outboxOptions
        ) : IConfigureOptions<QuartzOptions>
    {

        public void Configure(QuartzOptions options)
        {
const string jobName = nameof(ProcessOutboxMessagesJobSetUp);

options.AddJob<ProcessOutboxMessagesJob>(configure => configure.WithIdentity(jobName))
    .AddTrigger(configure =>
                    configure.ForJob(jobName)
                        .WithSimpleSchedule(schedule =>
                                                schedule.WithIntervalInSeconds(outboxOptions.Value.IntervalInSeconds)
                                                    .RepeatForever()));
        }
    }
}
