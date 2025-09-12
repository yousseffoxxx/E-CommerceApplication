using Domain.Exceptions;
using Microsoft.VisualBasic;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await _basketRepository.GetBasketAsync(key);

            if (basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(basket);
            else
                throw new BasketNotFoundException(key);
        }

        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = _mapper.Map<BasketDto, CustomerBasket>(basket);

            var CreateOrUpdateBasket = await _basketRepository.CreateOrUpdateBasketAsync(customerBasket);

            if (CreateOrUpdateBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can't Create Or Update Basket Now, Try Again Later");

        }

        public async Task<bool> DeleteBasketAsync(string key)
            => await _basketRepository.DeleteBasketAsync(key);
    }
}
