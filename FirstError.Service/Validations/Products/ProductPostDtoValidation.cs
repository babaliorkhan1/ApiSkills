using FirstError.Service.Dtos.Products;
using FirstError.Service.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Validations.Products
{
    public class ProductPostDtoValidation:AbstractValidator<ProductPostDto>
    {//path bir urlnin bir bolumunu veya routeun bir bolumunu temsil eder
        //mesela controller gibi bakilarsa route action a ise bir path gibi bakilir
        public ProductPostDtoValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x).Custom((x, context) =>
            {
                if (!x.formFile.IsImage())
                {
                    context.AddFailure("formFile", "Formfile is not image");
                }
                if (!x.formFile.IsSizeOk(2))
                {
                    context.AddFailure("formFile", "FormFile in not valid for 2 mb");
                }
            }); 
        }  //controllere girmemish ignore,qadaga ,blok sistemin islememisin yaradir,
           //elave olarag vaxt itkisi ve elave proes olmur
    }
}
