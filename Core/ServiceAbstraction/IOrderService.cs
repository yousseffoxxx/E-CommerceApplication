namespace ServiceAbstraction
{
    public interface IOrderService
    {
        // Get Order by id
        public Task<OrderDto> GetOrderByIdAsync(Guid id);
        // Get Orders for user By Email
        public Task<IEnumerable<OrderDto>> GetOrdersByEmailAsync(string email);
        // Create order
        public Task<OrderDto> CreateOrderAsync(OrderRequest orderRequest, string userEmail);
        // Get all Delivery Methods
        public Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
    }
}
