using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.Organizations;

namespace SkillTrackerServer.Infrastructure.Organizations
{
    internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(ur => ur.Id);

            builder.Property(ur => ur.Id).HasColumnName("id");
            builder.Property(ur => ur.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(ur => ur.OrganizationId).HasColumnName("organization_id").IsRequired();
            builder.Property(ur => ur.Role).HasColumnName("role").IsRequired();
            builder.Property(ur => ur.AssignedAt).HasColumnName("assigned_at");

            builder.HasIndex(ur => new { ur.UserId, ur.OrganizationId }).IsUnique();

            builder.HasIndex(ur => ur.OrganizationId);
        }
    }
}
