using AutoMapper;
using FirstApi.Data.Contexts;
using FirstApi.Service.Dtos.Categories;
using FirstApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstError.Api.Client.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "client_v1")]
    public class CategorysController : ControllerBase
    {
        //normalda route api controller yazdigda httpget ishleyir default olarag,
        //ama api controller action yazdigda htttpget ishlemir cunki routeda action teyin eliyirsen 

      private readonly ICategoryService _categoryService;
        public CategorysController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        //    static List<Category> dncwcvkmer = new List<Category> 
        //    {
        //        new Category{name="ewewec"},
        //        new Category{name="ewewec1"},
        //        new Category{name="ewewec1"}
        //    };
        //    [HttpGet]
        //    public async Task<IActionResult> GetAll()
        //    {
        //        return Ok(dncwcvkmer);
        //    }

        //    [HttpGet("{id}")]
        //    public async Task<IActionResult> Get(int id)
        //    {
        //        return Ok(dncwcvkmer[0]);    
        //    }
        //    [HttpPost]
        //    public async Task<IActionResult> Create([FromBody]Category category)
        //    {
        //        dncwcvkmer.Add(category);
        //        return Ok();
        //    }
        //    [HttpDelete]
        //    public async Task<IActionResult> Delete()
        //    {
        //        dncwcvkmer.Remove(dncwcvkmer[1]);
        //        return Ok(); 
        //    }

        //    public async Task<IActionResult> Update()
        //    {
        //        dncwcvkmer[1].name="sdcrcwrwewr";
        //        return Ok();
        //    }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          

            return StatusCode(200, await _categoryService.GetAllAsync());
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            //try
            //{

                var result = await _categoryService.GetAsync(id);

                return StatusCode(result.StatusCode, result);
            
            //catch (Exception ex)
            //{

            //    //return StatusCode(500, new {errormesage=ex.Message});
                
            //    return StatusCode(500, ex.Message);
            //}

            //her adddimbasi trycatch yazilmamaisi ucun
            //throw exception proyekti diyandjri
            //middleware arashdir

        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CategoryPostDto categoryPostDto)
        {

            var result = await _categoryService.CreateAsync(categoryPostDto);


            return StatusCode(result.StatusCode, result);//dbya nese elave edende gonderiloir
        }



    }
}
