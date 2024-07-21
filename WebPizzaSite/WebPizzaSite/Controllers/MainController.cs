using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebPizzaSite.Data;
using WebPizzaSite.Models.Category;

namespace WebPizzaSite.Controllers
{
    public class MainController : Controller
    {
        private readonly PizzaDbContext _pizzaDbContext;
        private readonly IMapper _mapper;

        public MainController(PizzaDbContext pizzaDbContext, IMapper mapper)
        {
            _pizzaDbContext = pizzaDbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var list = _pizzaDbContext.Categories
                .ProjectTo<CategoryItemViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return View(list);
        }

        /// <summary>
        /// Виводить сторінку для додавання нової категорії
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateViewModel model)
        {
            return View(model);
        }
    }
}
