namespace Domain.Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; } // GUID : Created From Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
