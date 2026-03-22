using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Organizations.RemoveMember
{
    public sealed record RemoveMemberCommand(Guid OrganizationId, Guid UserId) : ICommand;
}
