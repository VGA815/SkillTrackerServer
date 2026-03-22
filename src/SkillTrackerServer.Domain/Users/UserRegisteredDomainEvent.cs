using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Users
{
    public sealed record UserRegisteredDomainEvent(Guid UserId, string Email, string Username) : IDomainEvent;
}
