namespace Domain.Exceptions
{
    public class OrderNotFoundException<TKey> : NotFoundExceptions
    {
        public OrderNotFoundException(TKey key) : base($"Order with key {key} not found")
        {
        }
    }
}
