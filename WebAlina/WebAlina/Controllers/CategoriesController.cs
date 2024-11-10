using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAlina.Data;
using WebAlina.Data.Entities;
using WebAlina.Interfaces;
using WebAlina.Models.Category;

namespace WebAlina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public readonly AlinaDbContext _context;
        public readonly IConfiguration _configuration;
        public readonly IImageHulk _imageHulk;
        public readonly IMapper _mapper;
        public CategoriesController(AlinaDbContext context, IConfiguration configuration, 
            IMapper mapper, IImageHulk imageHulk)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _imageHulk = imageHulk;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ///var list = await _context.Categories
            ///    .Select(x=>new CategoryItemViewModel
            ///    {
            ///        Id = x.Id,
            ///        Name = x.Name,
            ///        Description = x.Description,
            ///        Image = x.Image,
            ///    })
            ///    .ToListAsync();

            var list = await _context.Categories
                .ProjectTo<CategoryItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CategoryCreateViewModel model)
        {
            string imageName = string.Empty;

            if (model.ImageFile != null)
            {
                imageName = await _imageHulk.Save(model.ImageFile);
            }
            var entity = _mapper.Map<CategoryEntity>(model);
            ///new CategoryEntity
            ///{
            ///    Name = model.Name,
            ///    Description = model.Description,
            ///    Image = imageName
            ///};
            entity.Image = imageName;
            _context.Categories.Add(entity);
            _context.SaveChanges();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            if(!string.IsNullOrEmpty(category.Image))
            {
                _imageHulk.Delete(category.Image);
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories
                .ProjectTo<CategoryItemViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] CategoryEditViewModel model)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == model.Id);
            if (category == null)
            {
                return NotFound();
            }
            _mapper.Map(model, category);
            if (model.ImageFile != null)
            {
                if (category.Image != null)
                {
                    _imageHulk.Delete(category.Image);
                }
                category.Image = await _imageHulk.Save(model.ImageFile);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
