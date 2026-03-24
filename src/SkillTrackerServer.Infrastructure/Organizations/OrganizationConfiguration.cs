using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.Organizations;

namespace SkillTrackerServer.Infrastructure.Organizations
{
    internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.ToTable("organizations");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(o => o.Description)
                .HasColumnName("description")
                .HasMaxLength(1000);
            builder.Property(o => o.CreatedAt).HasColumnName("created_at");
            builder.Property(o => o.UpdatedAt).HasColumnName("updated_at");

            builder.HasIndex(o => o.Name).IsUnique();
        }
    }
}
