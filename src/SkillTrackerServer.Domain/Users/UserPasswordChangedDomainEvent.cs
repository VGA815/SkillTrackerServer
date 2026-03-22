using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public sealed record UserPasswordChangedDomainEvent(Guid UserId) : IDomainEvent;
}
