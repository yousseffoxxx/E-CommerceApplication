namespace Service
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetCachedValueAsync(string cacheKey)
            => await _cacheRepository.GetAsync(cacheKey);


        public async Task SetCacheValueAsync(string cacheKey, object value, TimeSpan duration)
            => await _cacheRepository.SetAsync(cacheKey, value, duration);
    }
}
