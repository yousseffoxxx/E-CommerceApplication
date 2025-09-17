namespace Domain.Models.Order
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(OrderedProduct product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public OrderedProduct Product { get; set; } // Navigational Property
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
