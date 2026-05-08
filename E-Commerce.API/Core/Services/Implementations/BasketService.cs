using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Services.Implementations
{
    internal class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> AddOrUpdateBasketAsync(BasketDto basket)
        {
            var result = _mapper.Map<CustomerBasket>(basket);
            var CreateOrUpdate = await _basketRepository.AddOrUpdateAsync(result);
            return CreateOrUpdate is null ? throw new Exception("can not create or update basket") : _mapper.Map<BasketDto>(CreateOrUpdate);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            var result = await _basketRepository.DeleteAsync(id);
            return result ? result : throw new Exception($"can not Delete basket id {id}");
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var getBasket = await _basketRepository.GetBasketAsync(id);
            return getBasket is null ? throw new BasketNotFoundException(id) : _mapper.Map<BasketDto>(getBasket);
        }
    }
}
