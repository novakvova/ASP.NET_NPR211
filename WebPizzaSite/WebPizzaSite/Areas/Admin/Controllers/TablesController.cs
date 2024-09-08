using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TablesController : Controller
    {
        public IActionResult General()
        {
            return View();
        }
        public IActionResult Data()
        {
            return View();
        }
    }
}
