using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Goals.Update
{
    internal sealed class UpdateGoalCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<UpdateGoalCommand>
    {
        public async Task<Result> Handle(UpdateGoalCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status == DevelopmentPlanStatus.Archived)
                return Result.Failure(DevelopmentPlanErrors.CannotModifyArchived);

            Goal? goal = await context.Goals
                .SingleOrDefaultAsync(g => g.Id == command.GoalId, cancellationToken);

            if (goal is null)
                return Result.Failure(GoalErrors.NotFound(command.GoalId));

            if (goal.PlanId != command.PlanId)
                return Result.Failure(GoalErrors.DoesNotBelongToPlan);

            goal.Title       = command.Title;
            goal.Description = command.Description;
            goal.SkillArea   = command.SkillArea;
            goal.UpdatedAt   = dateTimeProvider.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
