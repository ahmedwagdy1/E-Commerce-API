namespace Domain.Exceptions
{
    public class ProductNotFoundException : NotFoundExceptions
    {
        public ProductNotFoundException(int id) : base($"Product with id {id} not found")
        {
            
        }
    }
}
