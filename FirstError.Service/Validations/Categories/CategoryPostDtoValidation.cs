using FirstApi.Service.Dtos.Categories;
using FluentValidation;

namespace FirstApi.Service.Validations.Categories
{
    public class CategoryPostDtoValidation : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not empty")
                .NotNull().WithMessage("Name cannot not null").MinimumLength(3).MaximumLength(20);
        }
    }
}
