using SkillTrackerServer.SharedKernel;

namespace SkillTrackerServer.Application.Abstractions.Data
{
    public interface ICacheService
    {
        Task<Result<T>?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan ttl);
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefix);
    }
}
