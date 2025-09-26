namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        // Create Or Update PaymentIntentId [in the Order]
        // takes BasketId and returns Basket with [PaymentIntentId + SecretKey]
        public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);

        public Task UpdateOrderPaymentStatus(string request, string stripeHeader);
    }
}
