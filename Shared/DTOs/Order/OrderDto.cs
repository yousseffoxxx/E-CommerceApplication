namespace Shared.DTOs.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
        public decimal SubTotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string Status { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public string DeliveryMethod { get; set; }
        public decimal Total { get; set; }
        public string PaymentStatus { get; set; }
    }
}
