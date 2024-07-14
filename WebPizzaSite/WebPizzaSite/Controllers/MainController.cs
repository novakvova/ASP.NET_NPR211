using Microsoft.AspNetCore.Mvc;
using WebPizzaSite.Data;

namespace WebPizzaSite.Controllers
{
    public class MainController : Controller
    {
        private readonly PizzaDbContext _pizzaDbContext;

        public MainController(PizzaDbContext pizzaDbContext)
        {
            _pizzaDbContext = pizzaDbContext;
        }
        public IActionResult Index()
        {
            var list = _pizzaDbContext.Categories.ToList();

            return View(list);
        }
    }
}
