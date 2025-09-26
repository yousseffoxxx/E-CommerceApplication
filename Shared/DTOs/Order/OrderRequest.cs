namespace Shared.DTOs.Order
{
    public class OrderRequest
    {
        public string BasketId { get; set; }
        public ShippingAddressDto shipToAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
