using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.DevelopmentPlans;

namespace SkillTrackerServer.Infrastructure.DevelopmentPlans
{
    internal sealed class DevelopmentPlanConfiguration : IEntityTypeConfiguration<DevelopmentPlan>
    {
        public void Configure(EntityTypeBuilder<DevelopmentPlan> builder)
        {
            builder.ToTable("development_plans");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.ManagerId).HasColumnName("manager_id").IsRequired();
            builder.Property(p => p.EmployeeId).HasColumnName("employee_id").IsRequired();
            builder.Property(p => p.OrganizationId).HasColumnName("organization_id").IsRequired();
            builder.Property(p => p.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(p => p.Description)
                .HasColumnName("description")
                .HasMaxLength(2000);
            builder.Property(p => p.Status).HasColumnName("status").IsRequired();
            builder.Property(p => p.StartDate).HasColumnName("start_date");
            builder.Property(p => p.EndDate).HasColumnName("end_date");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");


            builder.HasIndex(p => new { p.ManagerId, p.OrganizationId });
            builder.HasIndex(p => new { p.EmployeeId, p.OrganizationId });
        }
    }
}
