namespace Service
{
    public class OrderService(IUnitOfWork _unitOfWork,
        IMapper _mapper, IBasketRepository _basketRepository) : IOrderService
    {
        public async Task<IEnumerable<OrderDto>> GetOrdersByEmailAsync(string email)
        {
            var specifications = new OrderSpecifications(email);

            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(specifications);

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            var specifications = new OrderSpecifications(id);

            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specifications)
                ?? throw new OrderNotFoundException(id);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
        {
            var result = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

           return _mapper.Map<IEnumerable<DeliveryMethodDto>>(result);
        }

        public async Task<OrderDto> CreateOrUpdateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // address
            var address = _mapper.Map<ShippingAddress>(orderRequest.shipToAddress);

            // Order Items => Basket => Basket Items
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, product));
            }
            
            // Delivery method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            // check if there an Order with the same PaymentIntentId
            var exOrder = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId));
            
            if(exOrder is not null)
            {
                _unitOfWork.GetRepository<Order, Guid>().Remove(exOrder);
            }

            // sub Total
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // save to database
            var order = new Order(userEmail, address, subTotal, orderItems, deliveryMethod, basket.PaymentIntentId);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            // Map & return
            return _mapper.Map<OrderDto>(order);
        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
            => new OrderItem(new OrderedProduct(product.Id, product.Name, product.PictureUrl),item.Quantity,product.Price);
    }
}
