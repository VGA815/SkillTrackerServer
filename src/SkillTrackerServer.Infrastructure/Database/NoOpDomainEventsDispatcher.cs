using SkillTrackerServer.Infrastructure.DomainEvents;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Infrastructure.Database
{
    internal sealed class NoOpDomainEventsDispatcher : IDomainEventsDispatcher
    {
        public Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}