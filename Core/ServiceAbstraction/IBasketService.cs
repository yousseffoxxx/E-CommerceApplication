﻿namespace ServiceAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDto> GetBasketAsync(string key);

        public Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);

        public Task<bool> DeleteBasketAsync(string key);
    }
}
