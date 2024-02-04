using FirstApi.Service.Dtos.Categories;
using FirstApi.Service.Responses;
using FirstApi.Services.Interfaces;
using FirstError.Service.Dtos.Products;
using FirstError.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstError.Api.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "client_v1")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        // IIS, kullanıcıların web siteleri ve web uygulamaları barındırmalarını,
        // yönetmelerini ve dağıtmalarını sağlayan bir platformdur.

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            //tutalim bu yollla gedersizse eger ctorda cagir ve 
            //_logger.logInformation("product getall called")
            //logluyan zaman asp.smart da baxmag olur
            //ve biz logluyan zaman server americada oldugu icin emeliyyatin tarixin oz oldugu yere gore yazdi

            _logger.LogInformation("Getaall product is worked");
            return StatusCode(200,await _productService.GetAllAsync());
        }
        //bu loglama harda istifade edirik desen bezen olruki olanlari izlemek ucun ,
        // userin qarsisin acixan problemleri handle etmek ucun ,
        //olurki oz proyektimizden kapitalin sehifesine gecirik,
        //olurki odenish kecir ama baglanti sixintisi olur deye bizim sehifye gelmemish diyanir

        //soram onun odeniish edib etmediyini deqiqleshdirmek ucun baxilir ki heqiqietende  bu vaxti fln user odenish edib
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetAsync(id);

            return StatusCode(result.StatusCode, result);
        }


    }
}
