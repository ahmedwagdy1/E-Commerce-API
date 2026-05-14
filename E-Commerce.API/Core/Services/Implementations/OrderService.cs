using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;

namespace Services.Implementations
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmail)
        {
            // 1. map addressDto to address
            var address = _mapper.Map<Address>(order.ShipToAddress);

            // 2. GetOrderItems ==> BasketId ==> Basket ==> BasketItems[Id]
            var basket = await _basketRepository.GetBasketAsync(order.BasketId) 
                ?? throw new BasketNotFoundException(order.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) 
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(product, item));
            }
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            // 3. GetDeliveryMethod ==> DeliveryMethodId ==> DB
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(order.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(order.DeliveryMethodId);
            var orderExsist = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecification(basket.PaymentIntentId));
            if(orderExsist != null)
            {
                // delete record
                orderRepo.Delete(orderExsist);
            }

            // 4. Clculate SubTotal ==> OrderItems ==> OrderItems.Q * OrderItems.Price
            var subTotal = orderItems.Sum(o => o.Quantity * o.Price);

            // 5. create objet from Order ==> Parameter ==> Add DB ==> SaveChanges
            var orderToCreate = new Order(userEmail, address, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(orderToCreate);
            await _unitOfWork.SaveChangesAsync();

            // 6. map Order to OrderResult
            return _mapper.Map<OrderResult>(orderToCreate);
        }

        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            var orderInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(orderInOrderItem, product.Price, item.Quantity);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return deliveryMethods is null ? throw new Exception("Delivery methods not found") : _mapper.Map<IEnumerable<DeliveryMethodResult>>(deliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var orderById = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludesSpecification(id));
            return orderById is null ? throw new OrderNotFoundException<Guid>(id) : _mapper.Map<OrderResult>(orderById);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByIdEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpecification(userEmail));
            return orders is null ? throw new OrderNotFoundException<string>(userEmail) : _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
