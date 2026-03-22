using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Domain.Comments;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.Domain.InviteTokens;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.Domain.Tasks;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;

namespace SkillTrackerServer.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<UserPreference> UserPreferences { get; }
        DbSet<EmailVerificationToken> EmailVerificationTokens { get; }
        DbSet<InviteToken> InviteTokens { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Organization> Organizations { get; }
        DbSet<Goal> Goals { get; }
        DbSet<DevelopmentPlan> DevelopmentPlans { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Comment> Comments { get; }
        DbSet<DevelopmentTask> DevelopmentTasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
