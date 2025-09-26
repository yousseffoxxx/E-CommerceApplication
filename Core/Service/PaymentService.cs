using Stripe.Forwarding;

namespace Service
{
    public class PaymentService(IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork, IMapper _mapper,
        IConfiguration _configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            // 1. Get SecretKey
            StripeConfiguration.ApiKey = _configuration.GetRequiredSection("StripeSettings")["SecretKey"];

            // 2. Get Basket
            var basket = await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);

            // 3. Get SubTotal => Product
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                
                item.Price = product.Price;
            }

            // 4. Get DeliveryMethod
            if (!basket.DeliveryMethodId.HasValue)
                throw new DeliveryMethodNotFoundException("No Delivery Method Is Selected");

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            basket.ShippingPrice = deliveryMethod.Cost;

            // 5. Get Amount = SubTotal + DeliveryMethod.Price
            var amount = (long)(basket.Items.Sum(i => i.Quantity * i.Price) + basket.ShippingPrice) * 100;

            // 6. Create Or Update PaymentIntentId
            var paymentIntentService = new PaymentIntentService();
            if (string.IsNullOrWhiteSpace(basket.PaymentIntentId))
            {
                // Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await paymentIntentService.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;

                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };

                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }
            // 7. Save Basket Edits In Database
            await _basketRepository.CreateOrUpdateBasketAsync(basket);

            // 8. Map To BasketDto and return
            return _mapper.Map<Basket, BasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatus(string request, string stripeHeader)
        {
            var endPointSecret = _configuration.GetRequiredSection("StripeSettings")["EndPointSecret"];

            var stripeEvent = EventUtility.ConstructEvent(request, stripeHeader, endPointSecret);
            
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
            {
                await UpdatePaymentStatusSucceeded(paymentIntent.Id);
            }
            else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
            {
                await UpdatePaymentStatusFailed(paymentIntent.Id);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }
        }


        private async Task UpdatePaymentStatusSucceeded(string paymentIntentId)
        {
            var repo = _unitOfWork.GetRepository<Order, Guid>();

            var order = await repo.GetByIdAsync(new OrderWithPaymentIntentSpecifications(paymentIntentId))
                ?? throw new Exception(); //TODO

            order.Status = OrderPaymentStatus.PaymentReceived;

            repo.Update(order);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var repo = _unitOfWork.GetRepository<Order, Guid>();

            var order = await repo.GetByIdAsync(new OrderWithPaymentIntentSpecifications(paymentIntentId))
                ?? throw new Exception(); //TODO

            order.Status = OrderPaymentStatus.PaymentFailed;

            repo.Update(order);

            await _unitOfWork.SaveChangesAsync();        
        }     
    }
}
