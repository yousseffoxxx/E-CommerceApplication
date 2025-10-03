namespace Domain.Models.Order
{
    public class OrderedProduct
    {
        public OrderedProduct()
        {
            
        }
        public OrderedProduct(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

    }
}
