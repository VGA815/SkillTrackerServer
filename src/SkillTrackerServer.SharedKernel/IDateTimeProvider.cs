using System;

namespace SkillTrackerServer.SharedKernel
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
