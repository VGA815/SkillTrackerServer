using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Goals
{
    public static class GoalErrors
    {
        public static Error NotFound(Guid goalId) => Error.NotFound(
            "Goals.NotFound",
            $"The goal with the Id = '{goalId}' was not found");

        public static readonly Error DoesNotBelongToPlan = Error.Problem(
            "Goals.DoesNotBelongToPlan",
            "The specified goal does not belong to this development plan");
    }
}
