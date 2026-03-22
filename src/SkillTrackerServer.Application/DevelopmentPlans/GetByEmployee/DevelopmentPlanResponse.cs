using SkillTrackerServer.Domain.DevelopmentPlans;

namespace SkillTrackerServer.Application.DevelopmentPlans.GetByEmployee
{
    public sealed class DevelopmentPlanResponse
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid OrganizationId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DevelopmentPlanStatus Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
