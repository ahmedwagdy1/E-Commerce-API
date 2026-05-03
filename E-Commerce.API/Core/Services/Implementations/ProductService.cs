using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared;
using Shared.Dtos;
using Shared.Enums;

namespace Services.Implementations
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            // 1. IUnitOfWork ==> GetRepository<ProductBrand, int>() ==> IEnumerable<ProductBrand>
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            // 2. Map ProductBrand to BrandResultDto
            return _mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }

        public async Task<PagenationsResult<ProductResultDto>> GetAllProductsAsync(ProductsSpecificationsParameters parameters)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecifications(parameters);
            var products = await repo.GetAllAsync(specification);
            var prosctResult = _mapper.Map<IEnumerable<ProductResultDto>>(products);
            var pageSize = prosctResult.Count();
            var countSpecification = new ProductCountSpecification(parameters);
            var totalCount = await repo.CountAsync(countSpecification);
            return new PagenationsResult<ProductResultDto>(parameters.pageIndex, pageSize, totalCount, prosctResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(types);
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandAndTypeSpecifications(id);
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            return _mapper.Map<ProductResultDto>(product);
        }
    }
}
