using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Goals.Create
{
    internal sealed class CreateGoalCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<CreateGoalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateGoalCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure<Guid>(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure<Guid>(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status == DevelopmentPlanStatus.Archived)
                return Result.Failure<Guid>(DevelopmentPlanErrors.CannotModifyArchived);

            int nextOrder = await context.Goals
                .Where(g => g.PlanId == command.PlanId)
                .CountAsync(cancellationToken);

            Goal goal = Goal.Create(
                planId:      command.PlanId,
                title:       command.Title,
                description: command.Description,
                skillArea:   command.SkillArea,
                orderIndex:  nextOrder,
                createdAt:   dateTimeProvider.UtcNow);

            goal.Raise(new GoalCreatedDomainEvent(goal.Id, goal.PlanId));

            context.Goals.Add(goal);
            await context.SaveChangesAsync(cancellationToken);

            return goal.Id;
        }
    }
}
