﻿namespace Shared.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public string BrandName { get; set; } = default!;
        public string TypeName { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
