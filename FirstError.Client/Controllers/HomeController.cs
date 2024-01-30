using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstError.Client.Controllers
{
    public class HomeController : Controller
    {
       public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}