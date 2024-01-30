using FirstApi.Service.Responses;
using FirstError.Service.Dtos.Accounts;
using FirstError.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Login(LoginDto loginDto)
        {
            IdentityUser identityUser = await _userManager.FindByNameAsync(loginDto.UserName);

            if (identityUser == null)
            {
                return new ApiResponse { StatusCode = 404, Description="Username or password is incorrect" };
            }

            if (!await _userManager.CheckPasswordAsync(identityUser, loginDto.Password))
            {
                return new ApiResponse { StatusCode = 404, Description = "Username or password is incorrect" };
            }


            string keyStr = _configuration["Jwt:SecretKey"];

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,identityUser.UserName),
                new Claim(ClaimTypes.NameIdentifier,identityUser.Id),
            };


            var roles = await _userManager.GetRolesAsync(identityUser);

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(3),
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials

                );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //return new ApiResponse { StatusCode = 200, items = new { token = token } };
            return new ApiResponse { StatusCode = 200, items = token };
        }

        public async Task<ApiResponse> Register(RegisterDto registerDto)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, registerDto.Password);
            if (!result.Succeeded)
            {
                return new ApiResponse {StatusCode=400, items = result.Errors };
            }

            await _userManager.AddToRoleAsync(identityUser, "Admin");
            return new ApiResponse { StatusCode = 200 }; 
        }
    }
}
