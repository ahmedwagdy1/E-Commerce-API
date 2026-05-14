using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.BasketModule;

namespace Presentation.Controller
{
    [Authorize]
    public class PaymentsController(IServiceManger _serviceManger) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId)
            => Ok(await _serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));
    }
}
