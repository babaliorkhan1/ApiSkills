using FirstApi.Service.Dtos.Categories;
using FirstApi.Service.Responses;

namespace FirstApi.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<ApiResponse> CreateAsync(CategoryPostDto categoryPostDto);
        public Task<ApiResponse> DeleteAsync(int id);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id,CategoryUpdateDto dto);
    }
}
