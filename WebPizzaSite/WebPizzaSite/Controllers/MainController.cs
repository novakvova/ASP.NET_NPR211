using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using WebPizzaSite.Data;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Models.Category;

namespace WebPizzaSite.Controllers
{
    public class MainController : Controller
    {
        private readonly PizzaDbContext _pizzaDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public MainController(PizzaDbContext pizzaDbContext, IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _pizzaDbContext = pizzaDbContext;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(CategoryCreateViewModel model)
        {
            //Якщо модель валідна - тоді зберігаємо дані в БД
            if (ModelState.IsValid)
            {
                var cat = _mapper.Map<CategoryEntity>(model);

                if (model.Image != null && model.Image.Length > 0)
                {
                    var extension = Path.GetExtension(model.Image.FileName);
                    string fileName = $"{Guid.NewGuid().ToString()}{extension}";
                    // Define the path to save the image
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
                    var dir = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dir) && dir != null)
                    {
                        Directory.CreateDirectory(dir);
                    }
                    // Save the file
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    cat.Image = fileName;

                }

                _pizzaDbContext.Categories.Add(cat);
                _pizzaDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _pizzaDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (entity == null)
                return NotFound();
            _pizzaDbContext.Categories.Remove(entity);
            _pizzaDbContext.SaveChanges();
            return Ok();
        }
    }
}
