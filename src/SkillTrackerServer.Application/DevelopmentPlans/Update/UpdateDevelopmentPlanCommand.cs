using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.Update
{
    public sealed record UpdateDevelopmentPlanCommand(
        Guid PlanId,
        string Title,
        string? Description,
        DateTime? StartDate,
        DateTime? EndDate) : ICommand;
}
