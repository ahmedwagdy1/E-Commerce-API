using Shared.Enums;

namespace Shared
{
    public class ProductsSpecificationsParameters
    {
        private const int defultPageSize = 5;
        private const int maxPageSize = 10;
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public ProductSortingOptions sort { get; set; }
        public string? search { get; set; }
        public int pageIndex { get; set; } = 1;
        private int _pageSize = defultPageSize;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > maxPageSize ? defultPageSize : value; }
        }

    }
}
