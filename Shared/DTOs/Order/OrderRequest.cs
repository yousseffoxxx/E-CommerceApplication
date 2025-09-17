namespace Shared.DTOs.Order
{
    public class OrderRequest
    {
        public string BasketId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
