using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPizzaSite.Data;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Models.Product;

namespace WebPizzaSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly PizzaDbContext _pizzaDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        public ProductController(PizzaDbContext pizzaDbContext, IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _pizzaDbContext = pizzaDbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(ProductSearchViewModel search)
        {
            var query = _pizzaDbContext.Products.AsQueryable();
            int pageSize = 8;
            int page = search.Page ?? 1;
            page = page - 1;

            if(!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(search.Name.ToLower()));
            }

            int count = query.Count();  //усі запити в таблиці, які можна переглядати

            query = query.OrderBy(x=>x.Name).Skip(page*pageSize).Take(pageSize);
            var list = query
                .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            ProductsHomeViewModel model = new ProductsHomeViewModel()
            {
                Data = list,
                Count = count,
                Pagination = new Models.Helpers.PaginationViewModel
                {
                    PageSize = pageSize,
                    TotalItems = count
                },
                Search = new ProductSearchViewModel
                {
                    Name = search.Name,
                    Page = search.Page ?? 1
                }
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var catList = _pizzaDbContext.Categories
                .Select(x=>new {Value = x.Id, Text = x.Name})
                .ToList();

            ProductCreateViewModel model = new ProductCreateViewModel();
            model.CategoryList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(catList, "Value", "Text");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var prod = new ProductEntity
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };

            await _pizzaDbContext.Products.AddAsync(prod);
            await _pizzaDbContext.SaveChangesAsync();
            if (model.Photos != null)
            {
                int i = 0;

                foreach (var img in model.Photos)
                {
                    string ext = System.IO.Path.GetExtension(img.FileName);
                    string fileName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }
                    var imgEntity = new ProductImageEntity
                    {
                        Name = fileName,
                        Priority = i++,
                        Product = prod,
                    };
                    _pizzaDbContext.ProductImages.Add(imgEntity);
                    _pizzaDbContext.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
