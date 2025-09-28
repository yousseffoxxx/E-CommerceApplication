namespace ServiceAbstraction
{
    public interface ICacheService
    {
        public Task<string?> GetCachedValueAsync(string cacheKey);

        public Task SetCacheValueAsync(string cacheKey, object value, TimeSpan duration);
    }
}
