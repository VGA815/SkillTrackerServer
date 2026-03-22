using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Messaging;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Tasks.Create
{
    internal sealed class CreateTaskCommandHandler(
        IApplicationDbContext context,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<CreateTaskCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            DevelopmentPlan? plan = await context.DevelopmentPlans
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == command.PlanId, cancellationToken);

            if (plan is null)
                return Result.Failure<Guid>(DevelopmentPlanErrors.NotFound(command.PlanId));

            if (plan.ManagerId != userContext.UserId)
                return Result.Failure<Guid>(DevelopmentPlanErrors.AccessDenied);

            if (plan.Status == DevelopmentPlanStatus.Archived)
                return Result.Failure<Guid>(DevelopmentPlanErrors.CannotModifyArchived);

            Goal? goal = await context.Goals
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == command.GoalId, cancellationToken);

            if (goal is null)
                return Result.Failure<Guid>(GoalErrors.NotFound(command.GoalId));

            if (goal.PlanId != command.PlanId)
                return Result.Failure<Guid>(GoalErrors.DoesNotBelongToPlan);

            int nextOrder = await context.DevelopmentTasks
                .Where(t => t.GoalId == command.GoalId)
                .CountAsync(cancellationToken);

            DevelopmentTask task = DevelopmentTask.Create(
                goalId:      command.GoalId,
                planId:      command.PlanId,
                title:       command.Title,
                description: command.Description,
                dueDate:     command.DueDate,
                orderIndex:  nextOrder,
                createdAt:   dateTimeProvider.UtcNow);

            task.Raise(new TaskCreatedDomainEvent(task.Id, task.GoalId, task.PlanId));

            context.DevelopmentTasks.Add(task);
            await context.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }
}
