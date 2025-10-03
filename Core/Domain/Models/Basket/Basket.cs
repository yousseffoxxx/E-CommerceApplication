namespace Domain.Models.Basket
{
    public class Basket
    {
        public string Id { get; set; } // GUID : Created From Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
