namespace Domain.Models.Order
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, ShippingAddress shippingAddress, decimal subTotal, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            SubTotal = subTotal;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
        }

        // user Email
        public string UserEmail { get; set; }
        // Address
        public ShippingAddress ShippingAddress { get; set; }
        // SubTotal = Items.Q * Price
        public decimal SubTotal { get; set; }
        // OrderDate
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        // Payment Status
        public OrderPaymentStatus Status { get; set; } = OrderPaymentStatus.Pending;
        // Payment Intent
        public string PaymentIntentId { get; set; } = string.Empty;

        // Order Items / Navigational Property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        // Delivery Method / Navigational Property
        public DeliveryMethod DeliveryMethod { get; set; }

        // Foreign Key
        public int? DeliveryMethodId { get; set; }
    }
}
