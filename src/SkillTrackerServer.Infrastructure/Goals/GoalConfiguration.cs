using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.Goals;

namespace SkillTrackerServer.Infrastructure.Goals
{
    internal sealed class GoalConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.ToTable("goals");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id).HasColumnName("id");
            builder.Property(g => g.PlanId).HasColumnName("plan_id").IsRequired();
            builder.Property(g => g.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(300);
            builder.Property(g => g.Description)
                .HasColumnName("description")
                .HasMaxLength(2000);
            builder.Property(g => g.SkillArea)
                .HasColumnName("skill_area")
                .HasMaxLength(200);
            builder.Property(g => g.OrderIndex).HasColumnName("order_index");
            builder.Property(g => g.CreatedAt).HasColumnName("created_at");
            builder.Property(g => g.UpdatedAt).HasColumnName("updated_at");


            builder.HasIndex(g => new { g.PlanId, g.OrderIndex });
        }
    }
}
