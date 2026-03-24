using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.Domain.UserPreferences;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.Infrastructure.DomainEvents;
using SkillTrackerServer.SharedKernel;
using Microsoft.EntityFrameworkCore;
using SkillTrackerServer.Domain.InviteTokens;
using SkillTrackerServer.Domain.Organizations;
using SkillTrackerServer.Domain.Goals;
using SkillTrackerServer.Domain.DevelopmentPlans;
using SkillTrackerServer.Domain.Comments;
using SkillTrackerServer.Domain.Tasks;

namespace SkillTrackerServer.Infrastructure.Database
{
    public sealed class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher)
        : DbContext(options), IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserPreference> Preferences { get; set; }

        public DbSet<EmailVerificationToken> EmailVerificationTokens { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<UserPreference> UserPreferences {  get; set; }

        public DbSet<InviteToken> InviteTokens { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<DevelopmentPlan> DevelopmentPlans { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<DevelopmentTask> DevelopmentTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.HasDefaultSchema(Schemas.Default);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync();

            return result;
        }
        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(entity =>
                {
                    List<IDomainEvent> domainEvents = entity.DomainEvents;

                    entity.ClearDomainEvents();

                    return domainEvents;
                })
                .ToList();

            await domainEventsDispatcher.DispatchAsync(domainEvents);
        }
    }
}
