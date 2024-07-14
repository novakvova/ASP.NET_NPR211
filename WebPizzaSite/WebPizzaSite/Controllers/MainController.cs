using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
