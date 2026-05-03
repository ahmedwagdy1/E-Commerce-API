using Domain.Entities.ProductModule;
using Shared;

namespace Services.Specifications
{
    internal class ProductCountSpecification : BaseSpecifications<Product, int>
    {
        public ProductCountSpecification(ProductsSpecificationsParameters parameters) 
            : base(p => (!parameters.typeId.HasValue || p.TypeId == parameters.typeId) &&
                        (!parameters.brandId.HasValue || p.BrandId == parameters.brandId) &&
                        (string.IsNullOrEmpty(parameters.search) || p.Name.ToLower().Contains(parameters.search))
            )
        {
            
        }
    }
}
