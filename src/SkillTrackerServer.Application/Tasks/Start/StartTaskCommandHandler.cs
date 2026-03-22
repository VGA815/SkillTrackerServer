using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;
using TaskStatus = SkillTrackerServer.Domain.Tasks.TaskStatus;

namespace SkillTrackerServer.Application.Tasks.Start
{
    internal sealed class StartTaskCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<StartTaskCommand>
    {
        public async Task<Result> Handle(StartTaskCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure(DevelopmentPlanErrors.NotFound(command.PlanId));

            // Only the employee can start a task
            if (plan.EmployeeId != userContext.UserId)
                return Result.Failure(DevelopmentTaskErrors.AccessDenied);

            DevelopmentTask? task = await context.DevelopmentTasks
                .SingleOrDefaultAsync(t => t.Id == command.TaskId, cancellationToken);

            if (task is null)
                return Result.Failure(DevelopmentTaskErrors.NotFound(command.TaskId));

            if (task.GoalId != command.GoalId)
                return Result.Failure(DevelopmentTaskErrors.DoesNotBelongToGoal);

            if (task.Status != TaskStatus.NotStarted)
                return Result.Failure(DevelopmentTaskErrors.CannotStart(command.TaskId, task.Status));

            task.Status    = TaskStatus.InProgress;
            task.UpdatedAt = dateTimeProvider.UtcNow;

            task.Raise(new TaskStatusChangedDomainEvent(task.Id, task.GoalId, task.PlanId, TaskStatus.InProgress));

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
