using AutoMapper;
using Domain.Entities.IdentityModule;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Contracts;
using Shared.Common;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementations
{
    public class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> _options, IMapper _mapper) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            // Email Already Exsist
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnauthorizedException();
            // Check Password
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) throw new UnauthorizedException();
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email!);
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            // validate
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationsExceptions(errors);
            }

            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);
        }

        public async Task<bool> ChickEmailExistAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            return user != null;
        }

        public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail) ?? throw new UserNotFoundException(userEmail);
            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email!);
        }

        public async Task<AddressDto> GetUserAddressAsync(string userEmail)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == userEmail) 
                ?? throw new UserNotFoundException(userEmail);
            return _mapper.Map<AddressDto>(user.Address);
        }

        public async Task<AddressDto> UpdateUserAddressAsync(string userEmail, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                 .FirstOrDefaultAsync(u => u.Email == userEmail) 
                 ?? throw new UserNotFoundException(userEmail);

            if (user.Address is not null)  // update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else  // create
            {
                var address = _mapper.Map<Address>(addressDto);
                user.Address = address;
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<AddressDto>(user.Address);
        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = _options.Value;
            // Claims
            // Name , Email , Roles
            var claims = new List<Claim>()
            {
               new Claim(ClaimTypes.Name, user.DisplayName),
               new Claim(ClaimTypes.Email, user.Email!),
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var item in roles)
                claims.Add(new Claim(ClaimTypes.Role, item));

            // Key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));
            // Algorathm [Key , Algorathm]
            var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: jwtOptions.Issuer, audience: jwtOptions.Audience, claims: claims, expires: DateTime.UtcNow.AddDays(jwtOptions.ExpritionsInDays), signingCredentials: signInCred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
