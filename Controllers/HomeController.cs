using webodev.Models;
using Microsoft.AspNetCore.Mvc;

namespace webodev.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

       
        public IActionResult Hizmetlerimiz()
        {
            return View();
        }

    }
}