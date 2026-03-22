using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Goals
{
    public sealed class Goal : Entity
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? SkillArea { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static Goal Create(
            Guid planId,
            string title,
            string? description,
            string? skillArea,
            int orderIndex,
            DateTime createdAt)
        {
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                PlanId = planId,
                Title = title,
                Description = description,
                SkillArea = skillArea,
                OrderIndex = orderIndex,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

            return goal;
        }

        public Goal() { }
    }
}
