namespace Domain.Models.Order
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail, 
            ShippingAddress shipToAddress, 
            decimal subTotal, 
            ICollection<OrderItem> orderItems, 
            DeliveryMethod deliveryMethod, 
            string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            this.shipToAddress = shipToAddress;
            SubTotal = subTotal;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
        }

        // user Email
        public string UserEmail { get; set; }
        // Address
        public ShippingAddress shipToAddress { get; set; }
        // SubTotal = Items.Q * Price
        public decimal SubTotal { get; set; }
        // OrderDate
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        // Payment Status
        public OrderPaymentStatus Status { get; set; } = OrderPaymentStatus.Pending;
        // Payment Intent
        public string PaymentIntentId { get; set; }

        // Order Items / Navigational Property
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        // Delivery Method / Navigational Property
        public DeliveryMethod DeliveryMethod { get; set; }

        // Foreign Key
        public int? DeliveryMethodId { get; set; }
    }
}
