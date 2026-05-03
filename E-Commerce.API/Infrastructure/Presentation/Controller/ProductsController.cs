using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using Shared.Dtos;
using Shared.Enums;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]  // baseUrl/api/Products
    public class ProductsController(IServiceManger _serviceManger) : ControllerBase
    {
        // EndPoint ==> GetAllProducts
        [HttpGet("Products")] // baseUrl/api/Products/Products
        public async Task<ActionResult<PagenationsResult<ProductResultDto>>> GetAllProductsAsync([FromQuery] ProductsSpecificationsParameters parameters)
            => Ok(await _serviceManger.ProductService.GetAllProductsAsync(parameters));

        // EndPoint ==> GetAllBrands
        [HttpGet("Brands")] // baseUrl/api/Products/Brands
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManger.ProductService.GetAllBrandsAsync());

        // EndPoint ==> GetAllTypes
        [HttpGet("Types")] // baseUrl/api/Products/Types
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManger.ProductService.GetAllTypesAsync());

        // EndPoint ==> GetProductById
        [HttpGet("{id:int}")] // baseUrl/api/Products/5
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManger.ProductService.GetProductByIdAsync(id));
    }
}
