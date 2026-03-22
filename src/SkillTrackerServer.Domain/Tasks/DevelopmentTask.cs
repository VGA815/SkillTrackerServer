using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Tasks
{
    public sealed class DevelopmentTask : Entity
    {
        public Guid Id { get; set; }
        public Guid GoalId { get; set; }
        public Guid PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static DevelopmentTask Create(
            Guid goalId,
            Guid planId,
            string title,
            string? description,
            DateTime? dueDate,
            int orderIndex,
            DateTime createdAt)
        {
            var task = new DevelopmentTask
            {
                Id = Guid.NewGuid(),
                GoalId = goalId,
                PlanId = planId,
                Title = title,
                Description = description,
                Status = TaskStatus.NotStarted,
                DueDate = dueDate,
                OrderIndex = orderIndex,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

            return task;
        }
        public DevelopmentTask() { }
    }
}
