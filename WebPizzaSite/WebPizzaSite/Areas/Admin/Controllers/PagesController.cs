using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Frequently()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Error404()
        {
            return View();
        }
        public IActionResult Blank()
        {
            return View();
        }
    }
}
