﻿namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        
        public async Task<Basket?> GetBasketAsync(string key)
        {
            var basket = await _database.StringGetAsync(key);

            if (basket.IsNullOrEmpty)
                return null;

            else
                return JsonSerializer.Deserialize<Basket>(basket);
        }
        
        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(30));

            if (isCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);

            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string id) 
            => await _database.KeyDeleteAsync(id);     
    }
}
