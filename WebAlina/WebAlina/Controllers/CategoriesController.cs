using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAlina.Data;
using WebAlina.Data.Entities;
using WebAlina.Models.Category;

namespace WebAlina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly AlinaDbContext _context;
        public readonly IConfiguration _configuration;
        public CategoriesController(AlinaDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _context.Categories
                .Select(x=>new CategoryItemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CategoryCreateViewModel model)
        {
            string imageName = string.Empty;
            if (model.ImageFile != null)
            {
                imageName = Guid.NewGuid().ToString()+".jpg";
                var dirImage =_configuration["ImageFolder"] ?? "uploading";
                var fileSave = Path.Combine(Directory.GetCurrentDirectory(), dirImage, imageName);
                using(var stream = new FileStream(fileSave, FileMode.Create))
                    await model.ImageFile.CopyToAsync(stream);
            }
            var entity = new CategoryEntity
            {
                Name = model.Name,
                Description = model.Description,
                Image = imageName
            };
            _context.Categories.Add(entity);
            _context.SaveChanges();
            return Ok(entity);
        }
    }
}
