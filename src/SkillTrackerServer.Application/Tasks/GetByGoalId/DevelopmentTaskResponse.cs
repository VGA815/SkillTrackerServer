namespace SkillTrackerServer.Application.Tasks.GetByGoalId
{
    public sealed class DevelopmentTaskResponse
    {
        public Guid Id { get; set; }
        public Guid GoalId { get; set; }
        public Guid PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public Domain.Tasks.TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
