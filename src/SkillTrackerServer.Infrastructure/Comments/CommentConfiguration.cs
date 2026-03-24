using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillTrackerServer.Domain.Comments;

namespace SkillTrackerServer.Infrastructure.Comments
{
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id");
            builder.Property(c => c.TaskId).HasColumnName("task_id").IsRequired();
            builder.Property(c => c.AuthorId).HasColumnName("author_id").IsRequired();
            builder.Property(c => c.Body)
                .HasColumnName("body")
                .IsRequired()
                .HasMaxLength(4000);
            builder.Property(c => c.IsEdited).HasColumnName("is_edited");
            builder.Property(c => c.CreatedAt).HasColumnName("created_at");
            builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");

            builder.HasIndex(c => new { c.TaskId, c.CreatedAt });
        }
    }
}
