using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Tasks.Update
{
    internal sealed class UpdateTaskCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<UpdateTaskCommand>
    {
        public async Task<Result> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
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

            DevelopmentTask? task = await context.DevelopmentTasks
                .SingleOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

            if (task is null)
                return Result.Failure(DevelopmentTaskErrors.NotFound(command.TaskId));

            if (task.GoalId != command.GoalId)
                return Result.Failure(DevelopmentTaskErrors.DoesNotBelongToGoal);

            task.Title       = command.Title;
            task.Description = command.Description;
            task.DueDate     = command.DueDate;
            task.UpdatedAt   = dateTimeProvider.UtcNow;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
