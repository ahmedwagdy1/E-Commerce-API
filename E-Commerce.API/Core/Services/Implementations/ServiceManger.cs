using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using Services.Abstraction.Contracts;
using Services.Implementations;

namespace Services
{
    public class ServiceManger(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        public IProductService ProductService => _productService.Value;
    }
}
