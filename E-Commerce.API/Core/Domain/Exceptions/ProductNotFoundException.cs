namespace Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundExceptions
    {
        public ProductNotFoundException(int id) : base($"Product with id {id} not found")
        {
            
        }
    }
}
