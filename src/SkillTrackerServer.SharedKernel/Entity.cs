using System.Collections.Generic;

namespace SkillTrackerServer.SharedKernel
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public List<IDomainEvent> DomainEvents => [.. _domainEvents];

        public void Raise(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
