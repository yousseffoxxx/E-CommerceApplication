namespace Shared
{
    public class ProductQueryParams
    {
        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;

        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public ProductSortingOptions sortingOption { get; set; }
        public string? SearchValue { get; set; }

        private int pageIndex = 1;
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value < 1 ? 1 : value; }
        }

        private int pageSize = defaultPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }
    }
}
