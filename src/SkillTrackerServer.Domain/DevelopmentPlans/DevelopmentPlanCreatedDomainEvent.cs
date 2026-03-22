using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public sealed record DevelopmentPlanCreatedDomainEvent(Guid PlanId, Guid ManagerId, Guid EmployeeId, Guid OrganizationId) : IDomainEvent;
}
