namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _connection) : ICacheRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            var result = await _database.StringGetAsync(key);

            if (result.IsNullOrEmpty) return null;

            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var serializedObject = JsonSerializer.Serialize(value);

            await _database.StringSetAsync(key, serializedObject, duration);
        }
    }
}
