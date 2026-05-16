namespace Domain.Contracts
{
    public interface ICashRepository
    {
        //Get => Already cashed [return data] => Response Cash
        Task<string?> GetAsync(string key);
        //Set => No cashed hsppen [first time to call end point] => Applay Cash
        Task SetAsync(string key, object value, TimeSpan duration);
    }
}
