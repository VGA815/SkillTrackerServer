using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Infrastructure.DomainEvents
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
