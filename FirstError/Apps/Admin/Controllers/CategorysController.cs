using AutoMapper;
using FirstApi.Data.Contexts;
using FirstApi.Service.Dtos.Categories;
using FirstApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstError.Api.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [ApiController]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class CategorysController : ControllerBase
    {
      //sonradan tekrarda qeydlerim:
      //repositoires core da saxliyirsan ancaq interface hissesin
      //repositoreis db ile elaqeli hissedi deye data da ssalxiyirsan implemantationslaru4
       
       






        //========================================================================
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
            //IEnumerable ancak data okurken daha effektli netice verir ama
            //list kullanmak datalarin kenduisinde duzelishler gormek icindir birbasha olarag
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
                  var result= await _categoryService.GetAsync(id);


            return StatusCode(result.StatusCode,result);
        }


        [HttpPost]
        public async Task<IActionResult> Create( CategoryPostDto categoryPostDto)
        {

            var result=   await _categoryService.CreateAsync(categoryPostDto);

            
            return StatusCode(result.StatusCode,result );//dbya nese elave edende gonderiloir
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

           var result=await _categoryService.DeleteAsync(id);
            return StatusCode(result.StatusCode);   
          //  return StatusCode(204, category); //deyecey sozum yoxdu v
                                              //e ya no content methdouna  ishelede bilersen  
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]CategoryUpdateDto categoryUpdateDto)
        {

            //Category updatecategory = await categoryRepository.GetBYId(x => x.Id == id && !x.IsDeleted);

            //if (await categoryRepository.IsExist(x=> x.Id==id&& x.Name.Trim().ToLower()==categoryUpdateDto.Name.Trim().ToLower()))
            //{
            //    return StatusCode(400, new { description = $"{categoryUpdateDto.Name} is exist" });
            //} 
            //if (updatecategory  == null)
            //{ 
            //    return StatusCode(404);
            //}
            //map geriye yeni obyekt qaytarir deye id 0 dushur default olarag
            // updatecategory = _mapper.Map<Category>(categoryUpdateDto);//categoryupdate icinde //id yoxdu deye gelib
            //updatecategory.Name = categoryUpdateDto.Name;


            //categoryaya 0 menimsedir ve trackingde problem yaradir
            //await categoryRepository.Update(updatecategory);//update zamani mapperden istifade
            //elemek coxda coxsayilan hereketlerden deyil
            //onun evezine classic usul dha elverislidir


            //update oldugda tracking ramda ishlediyine gore
            //iki eyni idli obyekti izzleye bilmir ona gore
            //update isleminde hemishe diqqqqteli ol
            //await  categoryRepository.SaveAsync();
            var result = await _categoryService.UpdateAsync(id,categoryUpdateDto);
            return StatusCode(result.StatusCode);
        }

       
    }
}
