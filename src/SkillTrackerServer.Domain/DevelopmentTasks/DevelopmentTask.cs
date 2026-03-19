using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentTasks
{
    public sealed class DevelopmentTask : Entity
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public Guid SkillCategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public int Order { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime UpdatedAt { get; set; }
        public static DevelopmentTask Create(Guid planId, Guid skillCategoryId, string title, string? description, TaskPriority priority, int order, DateTime? deadline, DateTime updatedAt)
        {
            return new DevelopmentTask
            {
                Id = Guid.NewGuid(),
                PlanId = planId,
                SkillCategoryId = skillCategoryId,
                Title = title,
                Description = description,
                Priority = priority,
                Order = order,
                Deadline = deadline,
                UpdatedAt = updatedAt
            };
        }
        public DevelopmentTask()
        {
            
        }
    }
}
