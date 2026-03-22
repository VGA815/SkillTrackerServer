using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Domain.Comments;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.DevelopmentTasks;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.InviteTokens;
using SkillTrackerServer.Domain.ManagersEmployees;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.Domain.SkillCategories;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;

namespace SkillTrackerServer.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<UserPreference> UserPreferences { get; }
        DbSet<EmailVerificationToken> EmailVerificationTokens { get; }
        DbSet<SkillCategory> SkillCategories { get; }
        DbSet<DevelopmentPlan> DevelopmentPlans { get; }
        DbSet<Comment> Comments { get; }
        DbSet<DevelopmentTask> DevelopmentTasks { get; }
        DbSet<InviteToken> InviteTokens { get; }
        DbSet<ManagerEmployee> ManagerEmployees { get; }
        DbSet<Notification> Notifications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
