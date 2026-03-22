using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public static class DevelopmentPlanErrors
    {
        public static Error NotFound(Guid planId) => Error.NotFound(
            "DevelopmentPlans.NotFound",
            $"The development plan with the Id = '{planId}' was not found");

        public static Error CannotActivate(Guid planId, DevelopmentPlanStatus currentStatus) => Error.Problem(
            "DevelopmentPlans.CannotActivate",
            $"The plan '{planId}' cannot be activated from status '{currentStatus}'");

        public static Error AlreadyArchived(Guid planId) => Error.Conflict(
            "DevelopmentPlans.AlreadyArchived",
            $"The plan '{planId}' is already archived");

        public static readonly Error CannotModifyArchived = Error.Problem(
            "DevelopmentPlans.CannotModifyArchived",
            "An archived development plan cannot be modified");

        public static readonly Error AccessDenied = Error.Failure(
            "DevelopmentPlans.AccessDenied",
            "You do not have access to this development plan");
    }
}
