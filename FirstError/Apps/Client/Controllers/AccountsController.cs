using FirstError.Service.Dtos.Accounts;
using FirstError.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstError.Apps.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "client_v1")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto registerDto)
        {

            var result = await _accountService.Register(registerDto);
            return StatusCode(result.StatusCode, result);    
          
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var result = await _accountService.Login(loginDto);
            return StatusCode(result.StatusCode,result);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name="Admin"} );
        //    await _roleManager.CreateAsync(new IdentityRole { Name="SuperAdmin"} );
        //    await _roleManager.CreateAsync(new IdentityRole { Name="User"} );
        //    return Ok();
        //}
    }
}
