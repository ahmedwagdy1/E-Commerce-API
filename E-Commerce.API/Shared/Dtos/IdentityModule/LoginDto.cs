using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.IdentityModule
{
    public record LoginDto
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
