using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.Activate
{
    public sealed record ActivateDevelopmentPlanCommand(Guid PlanId) : ICommand;
}
