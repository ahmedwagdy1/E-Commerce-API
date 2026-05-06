namespace Shared.ErrorModel
{
    public class ValidationErrors
    {
        public string Field { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}