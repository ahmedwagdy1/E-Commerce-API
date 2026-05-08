using Domain.Entities.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        // Get basket by id
        Task<CustomerBasket?> GetBasketAsync(string id);
        // Add or Update basket
        Task<CustomerBasket?> AddOrUpdateAsync(CustomerBasket basket, TimeSpan? timeToLife = null);
        // Delete basket
        Task<bool> DeleteAsync(string id);
    }
}
