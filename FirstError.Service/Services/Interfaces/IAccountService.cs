using FirstApi.Service.Responses;
using FirstError.Service.Dtos.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Services.Interfaces
{
    public interface IAccountService
    {
        public  Task<ApiResponse> Register(RegisterDto registerDto);
        public  Task<ApiResponse> Login(LoginDto loginDto);

    }
}
