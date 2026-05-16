using Domain.Contracts;
using Services.Abstraction.Contracts;

namespace Services.Implementations
{
    public class CashService(ICashRepository _cashRepository) : ICashService
    {
        public async Task<string?> GetAsync(string key) => await _cashRepository.GetAsync(key);

        public async Task SetAsync(string key, object value, TimeSpan duration) => await _cashRepository.SetAsync(key, value, duration);
    }
}
