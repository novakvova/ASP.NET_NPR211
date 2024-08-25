using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebPizzaSite.Data;
using WebPizzaSite.Models.Product;

namespace WebPizzaSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly PizzaDbContext _pizzaDbContext;
        private readonly IMapper _mapper;
        public ProductController(PizzaDbContext pizzaDbContext, IMapper mapper)
        {
            _pizzaDbContext = pizzaDbContext;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var list = _pizzaDbContext.Products
                .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                .ToList();
            return View(list);
        }
    }
}
