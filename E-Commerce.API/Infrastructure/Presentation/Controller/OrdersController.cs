using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.OrderModule;
using System.Security.Claims;

namespace Presentation.Controller
{
    [Authorize]
    public class OrdersController(IServiceManger _serviceManger) : ApiController
    {
        // Create Order
        [HttpPost]
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest order)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManger.OrderService.CreateOrderAsync(order, userEmail!));
        }
            
        // Get Order By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
            => Ok(await _serviceManger.OrderService.GetOrderByIdAsync(id));

        // Get All Order By UserEmail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrderByUserEmail()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManger.OrderService.GetOrdersByIdEmailAsync(userEmail!));
        }
            
        // Get All DeliveryMethod
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetAllDeliveryMethod()
            => Ok(await _serviceManger.OrderService.GetDeliveryMethodsAsync());
    }
}
