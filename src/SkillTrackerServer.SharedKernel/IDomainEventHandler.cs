using System.Threading;
using System.Threading.Tasks;

namespace SkillTrackerServer.SharedKernel
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        Task Handle(T domainEvent, CancellationToken cancellationToken);
    }
}
