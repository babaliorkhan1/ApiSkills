using AutoMapper;
using FirstApi.Core.Entities;
using FirstApi.Service.Dtos.Categories;
using FirstError.Core.Entities;
using FirstError.Service.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Profiles.Products
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductPostDto, Product>();
            CreateMap<Product,ProductGetDto>();
        }
    }
}
