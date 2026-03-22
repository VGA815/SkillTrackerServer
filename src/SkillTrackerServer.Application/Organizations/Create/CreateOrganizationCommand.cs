using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Organizations.Create
{
    public sealed record CreateOrganizationCommand(string Name, string? Description) : ICommand<Guid>;

}
