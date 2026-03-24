using SkillTrackerServer.Domain.UserPreferences;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SkillTrackerServer.Infrastructure.UserPreferences
{
    internal sealed class UserPreferenceConfiguration : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder.ToTable("user_preferences");

            builder.HasKey(x => x.UserId);
        }
    }
}
