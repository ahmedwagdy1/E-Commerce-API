using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModule;
using Services.Abstraction.Contracts;
using Shared.Dtos;

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

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResultDto>>(products);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<TypeResultDto>>(types);
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            return _mapper.Map<ProductResultDto>(product);
        }
    }
}
