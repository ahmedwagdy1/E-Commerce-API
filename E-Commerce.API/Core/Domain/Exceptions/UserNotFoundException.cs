namespace Domain.Exceptions
{
    public class UserNotFoundException : NotFoundExceptions
    {
        public UserNotFoundException(string userEmail) : base($"user with userEmail {userEmail} not found")
        {
            
        }
    }
}
