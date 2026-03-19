using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.SkillCategories
{
    public static class SkillCategoryErrors
    {
        public static Error NotFound(Guid skillCategoryId) => Error.NotFound(
            "SkillCategories.NotFound",
            $"The skill category with the id = '{skillCategoryId}' was not found.");
    }
}
