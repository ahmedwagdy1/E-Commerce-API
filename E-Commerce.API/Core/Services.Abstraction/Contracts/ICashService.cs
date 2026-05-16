namespace Services.Abstraction.Contracts
{
    public interface ICashService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
