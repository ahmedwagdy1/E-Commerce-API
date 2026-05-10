using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.IdentityModule;

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
    }
}
