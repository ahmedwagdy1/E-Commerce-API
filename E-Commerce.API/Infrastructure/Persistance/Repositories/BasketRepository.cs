using Domain.Entities.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> AddOrUpdateAsync(CustomerBasket basket, TimeSpan? timeToLife = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var result = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLife ?? TimeSpan.FromDays(30));
            return result ? await GetBasketAsync(basket.Id) : null;
        }

        public async Task<bool> DeleteAsync(string id)
            => await _database.KeyDeleteAsync(id);
        
        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var result = await _database.StringGetAsync(id);
            if (result.IsNullOrEmpty) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(result!);
        }
    }
}
