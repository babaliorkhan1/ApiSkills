using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstError.Service.Dtos.Products
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public IFormFile? formFile { get; set; }
        public int CategoryId { get; set; }
    }
}
