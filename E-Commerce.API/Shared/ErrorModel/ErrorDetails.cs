using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shared.ErrorModel
{
    public class ErrorDetails
    {
        public int StatuseCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public override string ToString()
            => JsonSerializer.Serialize(this);
    }
}
