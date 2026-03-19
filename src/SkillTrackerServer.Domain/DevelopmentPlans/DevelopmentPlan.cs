using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public sealed class DevelopmentPlan : Entity
    {
        public Guid Id { get; set; }
        public Guid ManagerId { get; set; }
        public Guid EmployeeId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsArchived { get; set; }
        public DateTime UpdatedAt { get; set; }

        public static DevelopmentPlan Create(Guid managerId, Guid employeeId, string title, string? description, DateTime? deadline, DateTime updatedAt)
        {
            return new DevelopmentPlan
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                EmployeeId = employeeId,
                Title = title,
                Description = description,
                Deadline = deadline,
                IsArchived = false,
                UpdatedAt = updatedAt
            };
        }
        public DevelopmentPlan()
        {
            
        }
    }
}
