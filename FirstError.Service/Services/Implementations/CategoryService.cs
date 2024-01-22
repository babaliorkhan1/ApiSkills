using AutoMapper;
using FirstApi.Service.Dtos.Categories;
using FirstApi.Core.Entities;
using FirstApi.Core.Repositories1;
using FirstApi.Service.Responses;
using FirstApi.Services.Interfaces;

namespace FirstApi.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository1, IMapper mapper)
        {
            _mapper = mapper;
            categoryRepository = categoryRepository1;
        }
        

        public async Task<ApiResponse> CreateAsync(CategoryPostDto categoryPostDto)
        {
            if ( await categoryRepository.IsExist(x=>x.Name==categoryPostDto.Name))
            {
                return new ApiResponse { StatusCode=400,Description="Name is already exist" };
            }

            Category category=_mapper.Map<Category>(categoryPostDto);   
           await categoryRepository.AddAsync(category);
             
            await categoryRepository.SaveAsync();

            return new ApiResponse { StatusCode = 201,items=category };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            Category category = await categoryRepository.GetBYId(x => x.Id == id && !x.IsDeleted);


            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Item not found" };
            }

            category.IsDeleted = true;
           await categoryRepository.Update(category);  
            await categoryRepository.SaveAsync();
            return new ApiResponse { StatusCode = 200 };

        }

        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Category> query =await categoryRepository.GetAllAsync(x => !x.IsDeleted);//errro cixanda try catch sal orda daha yaxshi yazilir exception

             
            List<CategoryGetDto> categoriesget = new List<CategoryGetDto>();

            categoriesget =  query.Select(x => new CategoryGetDto { Name = x.Name }).ToList();

            return new ApiResponse { items = categoriesget,StatusCode=200 };

            //try
            //{
            //    List<CategoryGetDto> categoriesget = new List<CategoryGetDto>();

            //    categoriesget = query.Select(x => new CategoryGetDto { Name = x.Name }).ToList();

            //    return new ApiResponse { items = categoriesget, StatusCode = 200 };
            //}
            //catch (Exception ex)
            //{

            //    return null;
            //}
            //bele edib exception mesajina rahatlikla baxa bilersen


        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Category category = await categoryRepository.GetBYId(x => x.Id == id && !x.IsDeleted);


            if (category == null)
            {
                return  new ApiResponse { StatusCode=404,Description="Item not found"};
            }
            CategoryGetDto categoryGetDto = _mapper.Map<CategoryGetDto>(category);
            return new ApiResponse { items = categoryGetDto,StatusCode=200 };
        }

        public async Task<ApiResponse> UpdateAsync(int id,CategoryUpdateDto categoryUpdateDto)
        {
            Category category = await categoryRepository.GetBYId(x => x.Id == id && !x.IsDeleted);


            if (category == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Item not found" };
            }

            category.Name= categoryUpdateDto.Name; 
          
            await categoryRepository.Update(category);
            await categoryRepository.SaveAsync();
            return new ApiResponse { StatusCode = 200 };

        }

      
    }
}
