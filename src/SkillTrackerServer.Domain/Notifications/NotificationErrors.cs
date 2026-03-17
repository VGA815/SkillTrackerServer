using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.Notifications
{
    public static class NotificationErrors
    {
        public static Error NotFound(Guid notificationId) => Error.NotFound(
            "Notifications.NotFound",
            $"The notification with the id = '{notificationId}' was not found.");
    }
}
