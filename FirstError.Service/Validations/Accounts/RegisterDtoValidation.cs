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
    public class RegisterDtoValidation:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(x=>x.UserName).MinimumLength(8).MaximumLength(25).NotEmpty().NotNull();
            RuleFor(x => x).Custom((x, context) =>
            {
                Regex regex = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}(\\.[a-zA-Z]{2,})?$");
                if (!regex.IsMatch(x.Email))
                {
                    context.AddFailure("Email","Email is not valid");
                }
            });
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password!=x.ConfirmPassword)
                {
                    context.AddFailure("ConfirmPassword", "Password is not match");
                }
               
            });

        }
    }
}
