namespace Domain.Exceptions
{
    public sealed class ValidationsExceptions : Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];
        public ValidationsExceptions(IEnumerable<string> errors) : base("Validations Failed")
        {
            Errors = errors;
        }
    }
}
