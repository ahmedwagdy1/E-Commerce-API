using Services.Abstraction;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    public class ServiceMangerWithFactoryService(
        Func<IProductService> _productFactory,
        Func<IBasketService> _basketFactory,
        Func<IAuthenticationService> _authenticationFactory,
        Func<IOrderService> _orderFactory,
        Func<IPaymentService> _paymentFactory,
        Func<ICashService> _cashFactory) : IServiceManger
    {
        public IProductService ProductService => _productFactory.Invoke();

        public IBasketService BasketService => _basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationFactory.Invoke();

        public IOrderService OrderService => _orderFactory.Invoke();

        public IPaymentService PaymentService => _paymentFactory.Invoke();

        public ICashService CashService => _cashFactory.Invoke();
    }
}
