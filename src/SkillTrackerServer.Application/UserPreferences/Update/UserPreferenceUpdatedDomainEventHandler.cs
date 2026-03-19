using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.UserPreferences.Update
{
    //internal sealed class UserPreferenceUpdatedDomainEventHandler(ICacheService _cache) : IDomainEventHandler<UserPreferenceUpdatedDomainEvent>
    //{
    //    public async Task Handle(UserPreferenceUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    //    {
    //        var key = $"v1:user-preferences:{domainEvent.UserPreferenceId}";
    //        await _cache.RemoveAsync(key);
    //    }
    //}
}
