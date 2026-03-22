using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.DevelopmentPlans.Create
{
    public sealed record CreateDevelopmentPlanCommand(
        Guid EmployeeId,
        Guid OrganizationId,
        string Title,
        string? Description,
        DateTime? StartDate,
        DateTime? EndDate) : ICommand<Guid>;
}
