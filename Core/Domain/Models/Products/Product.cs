namespace Domain.Models.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        // Foreign Keys
        public int BrandId { get; set; }
        public int TypeId { get; set; }

        // Navigational Properties
        public ProductBrand ProductBrand { get; set; }
        public ProductType ProductType { get; set; }
    }
}
