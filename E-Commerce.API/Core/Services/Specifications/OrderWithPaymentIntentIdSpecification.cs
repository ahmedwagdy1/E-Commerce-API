using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
