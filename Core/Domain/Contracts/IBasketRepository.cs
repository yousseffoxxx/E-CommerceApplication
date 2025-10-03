namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<Basket?> GetBasketAsync(string key);

        public Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null);

        public Task<bool> DeleteBasketAsync(string id);
    }
}
