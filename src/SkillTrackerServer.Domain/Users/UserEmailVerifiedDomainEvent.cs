using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public sealed record UserEmailVerifiedDomainEvent(Guid UserId) : IDomainEvent;
}
