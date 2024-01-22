using FirstError.Service.Dtos.Accounts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstError.Service.Validations.Accounts
{
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(x=>x.UserName).MinimumLength(8).MaximumLength(25).NotEmpty().NotNull();
       
            RuleFor(x => x.Password).NotNull().NotEmpty();
           

        }
    }
}
