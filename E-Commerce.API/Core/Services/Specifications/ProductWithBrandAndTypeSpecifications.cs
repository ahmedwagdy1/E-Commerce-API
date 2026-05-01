using Domain.Entities.ProductModule;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications() : base(null)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
