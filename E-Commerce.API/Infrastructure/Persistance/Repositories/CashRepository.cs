
using StackExchange.Redis;
using System.Text.Json;

namespace Persistance.Repositories
{
    public class CashRepository(IConnectionMultiplexer _connectionMultiplexer) : ICashRepository
    {
        private readonly IDatabase _database = _connectionMultiplexer.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : value;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var valueJson = JsonSerializer.Serialize(value);  // C# => Json
            await _database.StringSetAsync(key, valueJson, duration);
        }
    }
}
