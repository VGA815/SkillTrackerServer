using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.DevelopmentPlans
{
    public sealed class DevelopmentPlan : Entity
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

        public static DevelopmentPlan Create(
            Guid managerId,
            Guid employeeId,
            Guid organizationId,
            string title,
            string? description,
            DateTime? startDate,
            DateTime? endDate,
            DateTime createdAt)
        {
            var plan = new DevelopmentPlan
            {
                Id = Guid.NewGuid(),
                ManagerId = managerId,
                EmployeeId = employeeId,
                OrganizationId = organizationId,
                Title = title,
                Description = description,
                Status = DevelopmentPlanStatus.Draft,
                StartDate = startDate,
                EndDate = endDate,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

            return plan;
        }

        public DevelopmentPlan() { }
    }
}
