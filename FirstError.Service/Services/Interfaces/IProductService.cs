using FirstApi.Service.Dtos.Categories;
using FirstApi.Service.Responses;
using FirstError.Service.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ApiResponse> CreateAsync(ProductPostDto productPostDto);
        public Task<ApiResponse> DeleteAsync(int id);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync();
        public Task<ApiResponse> UpdateAsync(int id,ProductUpdateDto dto);
    }
}
