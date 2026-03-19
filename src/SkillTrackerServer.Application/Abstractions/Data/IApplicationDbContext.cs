using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;

namespace SkillTrackerServer.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<UserPreference> UserPreferences { get; }
        DbSet<EmailVerificationToken> EmailVerificationTokens { get; }
        DbSet<Notification> Notifications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
