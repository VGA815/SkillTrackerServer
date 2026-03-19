using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public static class DevelopmentPlanErrors
    {
        public static Error NotFound(Guid planId) => Error.NotFound(
            "DevelopmentPlans.NotFound",
            $"The development plan with the Id = '{planId}' was not found");
    }
}
