using Services.Abstraction.Contracts;

namespace Services.Abstraction
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IOrderService OrderService { get; }
        public IPaymentService PaymentService { get; }
    }
}
