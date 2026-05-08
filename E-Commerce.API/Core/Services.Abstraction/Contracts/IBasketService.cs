using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts
{
    public interface IBasketService
    {
        // Get
        Task<BasketDto> GetBasketAsync(string id);
        // Add Or Update
        Task<BasketDto> AddOrUpdateBasketAsync(BasketDto basket);
        // Delete
        Task<bool> DeleteBasketAsync(string id);
    }
}
