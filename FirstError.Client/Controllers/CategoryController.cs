using FirstError.Client.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace FirstError.Client.Controllers
{
    public class ApiResponse
    {
        public string items { get; set; }
        //public string items { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }

    }

    //public class Test
    //{
    //    public string token { get; set; }
    //}
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class Random
    {
        public int StatusCode { get; set; }
    }
    public class CategoryController : Controller
    {


        private readonly string endpoint = "https://localhost:7124";
        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = new HttpClient();
            string token = Request.Cookies["token"];

            if (token == null)
            {
                LoginDto loginDto = new LoginDto();
                loginDto.UserName = "nihad123";
                loginDto.Password = "Nihad123@";
                var rsponse = await httpClient.PostAsJsonAsync<LoginDto>(endpoint + "/api/Accounts/Login", loginDto);


                var contents = await rsponse.Content.ReadFromJsonAsync<ApiResponse>();

                if (contents.StatusCode == 200)
                {
                    Response.Cookies.Append("token", contents.items);
                    token = contents.items;
                }

            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            GetItems<CategoryGetDto> getItems = new GetItems<CategoryGetDto>();

            //getItems.Items = new List<CategoryGetDto>();
            var response = await httpClient.GetStringAsync(endpoint + "/api/categorys");


            //JsonConvert.DeserializeObject<Person>(jsonString) ifadesini kullandığınızda,
            //bu JSON verisi Person sınıfına dönüştürülür. JSON içindeki "FirstName" değeri,
            //C# sınıfındaki FirstName özelliğine, "LastName" değeri LastName özelliğine,
            //"Age" değeri ise Age özelliğine eşleştirilir.

            getItems = JsonConvert.DeserializeObject<GetItems<CategoryGetDto>>(response);

            return View(getItems.Items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryPostDto postDto)
        {
            

            HttpClient httpClient = new HttpClient();


            var rsponse = await httpClient.PostAsJsonAsync<CategoryPostDto>(endpoint + "/api/Categorys/Create", postDto);


            string jdnjnw = "jcwnejcn";

            return RedirectToAction("Index", "Category");
        }
        [HttpGet]
        public async Task<IActionResult> Show()
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetStringAsync(endpoint + "/api/categorys");

            var reultss = JsonConvert.DeserializeObject<ApiResponse>(response);


            return View(reultss.items);
        }

       
    }

}