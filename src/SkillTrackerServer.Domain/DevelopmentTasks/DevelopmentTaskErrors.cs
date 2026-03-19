using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentTasks
{
    public static class DevelopmentTaskErrors
    {
        public static Error NotFound(Guid taskId) => Error.NotFound(
            "DevelopmentTasks.NotFound",
            $"The development task with the Id = '{taskId}' was not found");
    }
}
