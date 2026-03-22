using SkillTrackerServer.Application.Abstractions.Messaging;

namespace SkillTrackerServer.Application.Tasks.MarkOverdue
{
    /// Вызывается фоновым сервисом (background job) при наступлении DueDate.
    public sealed record MarkTaskOverdueCommand(Guid TaskId) : ICommand;
}
