using Domain.Entities.ProductModule;
using Shared;
using Shared.Enums;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductsSpecificationsParameters parameters) 
            : base(p => (!parameters.typeId.HasValue || p.TypeId == parameters.typeId) &&
                        (!parameters.brandId.HasValue || p.BrandId == parameters.brandId)&&
                        (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search))
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            
            switch (parameters.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagenations(parameters.pageIndex, parameters.pageSize);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
