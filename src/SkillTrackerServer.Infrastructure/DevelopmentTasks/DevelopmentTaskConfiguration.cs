using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.Tasks;

namespace SkillTrackerServer.Infrastructure.DevelopmentTasks
{
    internal sealed class DevelopmentTaskConfiguration : IEntityTypeConfiguration<DevelopmentTask>
    {
        public void Configure(EntityTypeBuilder<DevelopmentTask> builder)
        {
            builder.ToTable("development_tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.GoalId).HasColumnName("goal_id").IsRequired();
            builder.Property(t => t.PlanId).HasColumnName("plan_id").IsRequired();
            builder.Property(t => t.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(t => t.Description)
                .HasColumnName("description")
                .HasMaxLength(2000);
            builder.Property(t => t.Status).HasColumnName("status").IsRequired();
            builder.Property(t => t.DueDate).HasColumnName("due_date");
            builder.Property(t => t.CompletedAt).HasColumnName("completed_at");
            builder.Property(t => t.OrderIndex).HasColumnName("order_index");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at");

            builder.HasIndex(t => new { t.GoalId, t.OrderIndex });
            builder.HasIndex(t => new { t.Status, t.DueDate });
        }
    }
}
