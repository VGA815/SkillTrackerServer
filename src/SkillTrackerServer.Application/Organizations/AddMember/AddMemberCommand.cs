using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.Organizations;

namespace SkillTrackerServer.Application.Organizations.AddMember
{
    public sealed record AddMemberCommand(
        Guid OrganizationId,
        Guid UserId,
        OrganizationRole Role) : ICommand;
}
