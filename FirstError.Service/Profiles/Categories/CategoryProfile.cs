using AutoMapper;
using FirstApi.Core.Entities;
using FirstApi.Service.Dtos.Categories;

namespace FirstApi.Service.Profiles.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryPostDto,Category>();
            CreateMap<CategoryUpdateDto,Category>();
            CreateMap<Category,CategoryGetDto>();
        }
    } 
}
