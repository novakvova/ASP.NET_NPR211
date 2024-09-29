using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAlina.Data;
using WebAlina.Models.Category;

namespace WebAlina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly AlinaDbContext _context;
        public CategoriesController(AlinaDbContext context)
        {
            _context = context;
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
    }
}
