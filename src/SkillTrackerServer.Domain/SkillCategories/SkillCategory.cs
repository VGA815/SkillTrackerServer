using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.SkillCategories
{
    public sealed class SkillCategory : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public static SkillCategory Create(string name, string? description = null)
        {
            return new SkillCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
        }
        public SkillCategory()
        {
            
        }
    }
}
