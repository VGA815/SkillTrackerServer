using SkillTrackerServer.Application.Abstractions.Authentication;
using SkillTrackerServer.Application.Abstractions.Data;
using SkillTrackerServer.Application.Abstractions.Notifications;
using SkillTrackerServer.Domain.EmailVerificationTokens;
using SkillTrackerServer.Domain.Notifications;
using SkillTrackerServer.Domain.Users;
using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Users.Register
{
    internal sealed class UserRegisteredDomainEventHandler(IApplicationDbContext context, IEmailSender emailSender, IDateTimeProvider dateTimeProvider, INotificationSender notificationSender) : IDomainEventHandler<UserRegisteredDomainEvent>
    {
        public async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            EmailVerificationToken token = new()
            {
                TokenId = Guid.NewGuid(),
                UserId = domainEvent.UserId,
                CreatedAt = dateTimeProvider.UtcNow,
                ExpiresAt = dateTimeProvider.UtcNow.AddMinutes(20)
            };
            Notification notification = new()
            {
                Id = Guid.NewGuid(),
                UserId = domainEvent.UserId,
                Type = "Welcome",
                Title = "Welcome to SkillTracker!",
                Body = "Thank you for registering. Please verify your email address to get started.",
                ReferenceId = null,
                IsRead = false,
                CreatedAt = dateTimeProvider.UtcNow
            };

            context.EmailVerificationTokens.Add(token);
            context.Notifications.Add(notification);
            await context.SaveChangesAsync(cancellationToken);


            await emailSender.SendVerification(domainEvent.Email, token.TokenId.ToString());
            await notificationSender.SendAsync(notification.Id, notification.UserId, notification.Type, notification.Title, notification.Body, notification.CreatedAt, notification.ReferenceId, cancellationToken);
        }
    }
}
