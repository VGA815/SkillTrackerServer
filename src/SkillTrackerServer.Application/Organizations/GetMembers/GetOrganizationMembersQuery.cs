using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Organizations.GetMembers
{
    public sealed record GetOrganizationMembersQuery(Guid OrganizationId) : IQuery<List<OrganizationMemberResponse>>;
}
