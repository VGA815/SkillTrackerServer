using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.Archive
{
    public sealed record ArchiveDevelopmentPlanCommand(Guid PlanId) : ICommand;
}
