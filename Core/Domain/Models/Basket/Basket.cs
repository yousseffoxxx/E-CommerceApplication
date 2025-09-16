namespace Domain.Models.Basket
{
    public class Basket
    {
        public string Id { get; set; } // GUID : Created From Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
