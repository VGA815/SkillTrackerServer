namespace SkillTrackerServer.Application.Abstractions.Messaging
{
    public interface ICacheableQuery
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
