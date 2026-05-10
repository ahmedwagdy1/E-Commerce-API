using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Shared.Dtos.ProductModule;

namespace Presentation.Controller
{
    [Authorize]
     // baseUrl/api/Basket
    public class BasketController(IServiceManger _serviceManger) : ApiController
    {
        // Get  BaseUrl/api/Basket
        [HttpGet]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManger.BasketService.GetBasketAsync(id));

        // Post  BaseUrl/api/Basket
        [HttpPost]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> AddOrUpdateBasketAsync(BasketDto basketDto)
            => Ok(await _serviceManger.BasketService.AddOrUpdateBasketAsync(basketDto));

        // Delete  BaseUrl/api/Basket/basket01
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
        {
            await _serviceManger.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
