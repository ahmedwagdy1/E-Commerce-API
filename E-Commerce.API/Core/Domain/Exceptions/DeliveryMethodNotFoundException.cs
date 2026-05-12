namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException : NotFoundExceptions
    {
        public DeliveryMethodNotFoundException(int id) : base($"DeliveryMethod with id {id} not found")
        {
            
        }
    }
}
