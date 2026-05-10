namespace Shared.Common
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double ExpritionsInDays { get; set; }
    }
}
