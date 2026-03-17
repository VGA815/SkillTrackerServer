using SkillTrackerServer.SharedKernel;
using System;

namespace SkillTrackerServer.Domain.UserPreferences
{
    public sealed class UserPreference : Entity
    {
        public Guid UserId { get; set; }
        public UserPreferenceTheme Theme { get; set; }
        public bool ReceiveNotifications { get; set; }
        public static UserPreference Create(Guid userId, UserPreferenceTheme theme, bool receiveNotifications)
            => new () { ReceiveNotifications = receiveNotifications, UserId = userId, Theme = theme };
        public static UserPreference CreateDefault(Guid userId)
            => new () { ReceiveNotifications = true, UserId = userId, Theme = UserPreferenceTheme.System };
        public UserPreference() { }
    }
}
