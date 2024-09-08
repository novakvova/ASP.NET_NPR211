using Microsoft.AspNetCore.Mvc;

namespace WebPizzaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartsController : Controller
    {
        public IActionResult ChartJS()
        {
            return View();
        }
        public IActionResult ApexCharts()
        {
            return View();
        }
        public IActionResult ECharts()
        {
            return View();
        }
    }
}
