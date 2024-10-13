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
                //imageName = Guid.NewGuid().ToString()+".jpg";
                //var dirImage =_configuration["ImageFolder"] ?? "uploading";
                //var fileSave = Path.Combine(Directory.GetCurrentDirectory(), dirImage, imageName);
                //using(var stream = new FileStream(fileSave, FileMode.Create))
                //    await model.ImageFile.CopyToAsync(stream);
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
    }
}
