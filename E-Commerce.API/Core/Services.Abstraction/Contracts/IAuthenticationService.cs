using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;

namespace Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        // Login     return UserResultDto [DisplayName , Token , Email] take parameter [Email , Password]
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        // Register  return UserResultDto [DisplayName , Token , Email] take parameter [Email , Password , PhoneNamber, DisplayName, UserName]
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        // Get Current User
        Task<UserResultDto> GetCurrentUserAsync(string userEmail);
        // Chick if email exist
        Task<bool> ChickEmailExistAsync(string userEmail);
        // Get Address
        Task<AddressDto> GetUserAddressAsync(string userEmail);
        // Update Address
        Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto);
    }
}
