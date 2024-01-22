using AutoMapper;
using FirstApi.Core.Repositories1;
using FirstApi.Service.Responses;
using FirstError.Core.Entities;
using FirstError.Core.Repositories1;
using FirstError.Service.Dtos.Products;
using FirstError.Service.Extensions;
using FirstError.Service.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FirstError.Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHost;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _log;
        public ProductService(IMapper mapper, IProductRepository productRepository,IWebHostEnvironment webHost, ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor, ILogService log)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _webHost = webHost;
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }

        public async Task<ApiResponse> CreateAsync(ProductPostDto productPostDto)
        {
            //var identityUser = _httpContextAccessor.HttpContext.User.Identities.FirstOrDefault();
            string userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _httpContextAccessor.HttpContext.User.Identity.Name;
            Product product = _mapper.Map<Product>(productPostDto);
            product.Image = productPostDto.formFile.SaveFile(_webHost.WebRootPath, "assets/images");
          
            product.ImageUrl = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + $"/assets/images/{product.Image}";
            //Eğer bu değer bir HTML belgesi içinde kullanılıyorsa,
            //tarayıcı bu yolun başında bulunan kısmı, yani / assets / images / 'i mevcut sayfanın kök konumuna ekler.
            //Yani, bu durumda, tarayıcı bu href değerini, mevcut sayfanın kök konumuna ekleyerek,
            //örneğin https://www.example.com/assets/images/234.jpg gibi bir URL'ye dönüştürür.

            await  _productRepository.AddAsync(product);
            await _log.CreateAsync(new Log { Action = "Created Product", UserId = userid });
            //await _log.CreateAsync(new Log { Action = "Created Product", UserId = $"{identityUser}"});
            if (! await _categoryRepository.IsExist(x => x.Id == productPostDto.CategoryId))
            {
                return new ApiResponse { StatusCode = 404 ,Description="Category id is not valid" };
            }
            await   _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 201 };
        }

        public async Task<ApiResponse> DeleteAsync(int id)
        {
            Product product = await _productRepository.GetBYId(x => !x.IsDeleted && x.Id == id, "Category");
            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Product is not valid" };
            }
            product.IsDeleted = true;
            await _productRepository.Update(product);
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }

        public async Task<ApiResponse> GetAllAsync()
        {
            var query = await _productRepository.GetAllAsync(x=>!x.IsDeleted,"Category");

            IEnumerable<ProductGetDto> productGetDtos =await query
                .Select
                (x =>new ProductGetDto{ Id=x.Id, Image = x.Image, CategoryId = x.CategoryId, Name = x.Name, Price = x.Price,CategoryName=x.Category.Name,ImageUrl=x.ImageUrl  })
                .ToListAsync();
            return new ApiResponse { StatusCode = 200, items = productGetDtos };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Product product =await _productRepository.GetBYId(x => !x.IsDeleted && x.Id == id, "Category");
            if (product==null)
            {
                return new ApiResponse { StatusCode = 404,Description="Product is not valid" };
            }
            ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);
            productGetDto.CategoryName = product.Category.Name;
            return new ApiResponse { StatusCode = 200, items = productGetDto };

        }

        public async Task<ApiResponse> UpdateAsync(int id, ProductUpdateDto dto)
        {
            string userid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Product product = await _productRepository.GetBYId(x => !x.IsDeleted && x.Id == id, "Category");
            if (product == null)
            {
                return new ApiResponse { StatusCode = 404, Description = "Product is not valid" };
            }
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;
            product.Image = dto.formFile == null ? product.Image : dto.formFile.SaveFile(_webHost.WebRootPath, "assets/images"); 
          
            await _productRepository.Update(product);
          await  _log.CreateAsync(new Log { Action = "Updated Product", UserId = userid });
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
