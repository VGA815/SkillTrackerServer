using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public static class DevelopmentTaskErrors
    {
        public static Error NotFound(Guid taskId) => Error.NotFound(
            "Tasks.NotFound",
            $"The task with the Id = '{taskId}' was not found");

        public static Error CannotStart(Guid taskId, TaskStatus currentStatus) => Error.Problem(
            "Tasks.CannotStart",
            $"The task '{taskId}' cannot be started from status '{currentStatus}'");

        public static Error AlreadyCompleted(Guid taskId) => Error.Conflict(
            "Tasks.AlreadyCompleted",
            $"The task '{taskId}' is already completed");

        public static Error CannotComplete(Guid taskId, TaskStatus currentStatus) => Error.Problem(
            "Tasks.CannotComplete",
            $"The task '{taskId}' cannot be completed from status '{currentStatus}'");

        public static Error CannotMarkOverdue(Guid taskId, TaskStatus currentStatus) => Error.Problem(
            "Tasks.CannotMarkOverdue",
            $"The task '{taskId}' cannot be marked as overdue from status '{currentStatus}'");

        public static readonly Error DoesNotBelongToGoal = Error.Problem(
            "Tasks.DoesNotBelongToGoal",
            "The specified task does not belong to this goal");

        public static readonly Error AccessDenied = Error.Failure(
            "Tasks.AccessDenied",
            "You do not have access to this task");
    }
}
