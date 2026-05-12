using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithIncludesSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderWithIncludesSpecification(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
        public OrderWithIncludesSpecification(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
