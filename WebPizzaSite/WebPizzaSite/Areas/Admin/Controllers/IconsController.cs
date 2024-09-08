using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IconsController : Controller
    {
        public IActionResult Bootstrap()
        {
            return View();
        }
        public IActionResult Remix()
        {
            return View();
        }
        public IActionResult Boxicons()
        {
            return View();
        }
    }
}
