using FirstApi.Service.Dtos.Categories;
using FirstApi.Service.Responses;
using FirstApi.Services.Interfaces;
using FirstError.Service.Dtos.Products;
using FirstError.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstError.Api.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [ApiController]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            return StatusCode(200,await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetAsync(id);




            return StatusCode(result.StatusCode, result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDto productPostDto)
        {

            var result = await _productService.CreateAsync(productPostDto);

            return StatusCode(result.StatusCode, result);//dbya nese elave edende gonderiloir
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            return StatusCode(result.StatusCode);
            //  return StatusCode(204, product); //deyecey sozum yoxdu v
            //e ya no content methdouna  ishelede bilersen  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            //product updateproduct = await productRepository.GetBYId(x => x.Id == id && !x.IsDeleted);

            //if (await productRepository.IsExist(x=> x.Id==id&& x.Name.Trim().ToLower()==productUpdateDto.Name.Trim().ToLower()))
            //{
            //    return StatusCode(400, new { description = $"{productUpdateDto.Name} is exist" });
            //} 
            //if (updateproduct  == null)
            //{ 
            //    return StatusCode(404);
            //}
            //map geriye yeni obyekt qaytarir deye id 0 dushur default olarag
            // updateproduct = _mapper.Map<product>(productUpdateDto);//productupdate icinde //id yoxdu deye gelib
            //updateproduct.Name = productUpdateDto.Name;


            //productaya 0 menimsedir ve trackingde problem yaradir
            //await productRepository.Update(updateproduct);//update zamani mapperden istifade
            //elemek coxda coxsayilan hereketlerden deyil
            //onun evezine classic usul dha elverislidir


            //update oldugda tracking ramda ishlediyine gore
            //iki eyni idli obyekti izzleye bilmir ona gore
            //update isleminde hemishe diqqqqteli ol
            //await  productRepository.SaveAsync();
            var result = await _productService.UpdateAsync(id, productUpdateDto);
            return StatusCode(result.StatusCode);
        }

    }
}
