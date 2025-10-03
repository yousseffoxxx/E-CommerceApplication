using Microsoft.VisualBasic;

namespace Presentation.Controllers
{
    // BaseUrl/api/Orders
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        // Create Order
        // POST BaseUrl/api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.CreateOrUpdateOrderAsync(orderRequest, email);

            return Ok(result);
        }

        // Get Orders By email
        // GET BaseUrl/api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.GetOrdersByEmailAsync(email);

            return Ok(result);
        }

        // Get Order By id
        // GET BaseUrl/api/Orders/id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(Guid id)
        {
            var result = await _serviceManager.OrderService.GetOrderByIdAsync(id);

            return Ok(result);
        }

        // Get all Delivery Methods
        // GET BaseUrl/api/Orders/DeliveryMethods
        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var result = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();

            return Ok(result);
        }

    }
}
