using Services.Abstraction.Contracts;

namespace Services.Abstraction
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
    }
}
