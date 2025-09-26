namespace Presentation.Controllers
{
    // BaseUrl/api/Basket
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        // Get Basket
        [HttpGet] // GET BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);

            return Ok(basket);
        }

        // Create or Update Basket
        [HttpPost] // POST BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);

            return Ok(Basket);
        }

        // Delete Basket
        [HttpDelete("{Key}")] // DELETE BaseUrl/api/Basket/gjksdlgkd246k
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(Key);

            return Ok(result);
        }
    }
}
