
using FluentValidation;

namespace FirstApi.Service.Dtos.Categories
{
    public class CategoryPostDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
