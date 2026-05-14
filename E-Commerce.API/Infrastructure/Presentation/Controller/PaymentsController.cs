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

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var signatureHeader = Request.Headers["Stripe-Signature"];

            await _serviceManger.PaymentService.UpdatePaymentStatusAsync(json, signatureHeader);
            return new EmptyResult();
        }

    }
}
