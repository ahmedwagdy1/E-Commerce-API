using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;
using Services.Abstraction.Contracts;
using Services.Implementations;

namespace Services
{
    public class ServiceManger(IUnitOfWork _unitOfWork, IMapper _mapper, IBasketRepository _basketRepository) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
