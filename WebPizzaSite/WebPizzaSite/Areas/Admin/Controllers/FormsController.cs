using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormsController : Controller
    {
        public IActionResult Elements()
        {
            return View();
        }
        public IActionResult Layouts()
        {
            return View();
        }
        public IActionResult Editors()
        {
            return View();
        }
        public IActionResult Validation()
        {
            return View();
        }
    }
}
