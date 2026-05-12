using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class AuthenticationController(IServiceManger _serviceManger) : ApiController
    {
        // post => login
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> LoginAsync(LoginDto loginDto)
            => Ok(await _serviceManger.AuthenticationService.LoginAsync(loginDto));

        // post => register
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> RegisterAsync(RegisterDto registerDto) 
            => Ok(await _serviceManger.AuthenticationService.RegisterAsync(registerDto));

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync(string email)
            => Ok(await _serviceManger.AuthenticationService.ChickEmailExistAsync(email));
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResultDto>> GetCurrentUserAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManger.AuthenticationService.GetCurrentUserAsync(userEmail!));
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManger.AuthenticationService.GetUserAddressAsync(userEmail!));
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await _serviceManger.AuthenticationService.UpdateUserAddressAsync(userEmail!, addressDto));
        }
    }
}
