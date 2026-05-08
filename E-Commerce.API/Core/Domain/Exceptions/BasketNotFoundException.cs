namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundExceptions
    {
        public BasketNotFoundException(string id) : base($"basket with id {id} not found")
        {
            
        }
    }
}
