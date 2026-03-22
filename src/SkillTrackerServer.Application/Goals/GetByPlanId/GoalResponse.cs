namespace SkillTrackerServer.Application.Goals.GetByPlanId
{
    public sealed class GoalResponse
    {
        public Guid Id { get; set; }
        public Guid PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? SkillArea { get; set; }
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
