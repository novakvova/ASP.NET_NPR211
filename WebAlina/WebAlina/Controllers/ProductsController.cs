using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAlina.Data;
using WebAlina.Models.Product;

namespace WebAlina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly AlinaDbContext _context;
        public readonly IConfiguration _configuration;
        public readonly IMapper _mapper;
        public ProductsController(AlinaDbContext context, IConfiguration configuration, 
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var list = await _context.Products
                .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(list);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm]CategoryCreateViewModel model)
        //{
        //    string imageName = string.Empty;
        //    if (model.ImageFile != null)
        //    {
        //        imageName = Guid.NewGuid().ToString()+".jpg";
        //        var dirImage =_configuration["ImageFolder"] ?? "uploading";
        //        var fileSave = Path.Combine(Directory.GetCurrentDirectory(), dirImage, imageName);
        //        using(var stream = new FileStream(fileSave, FileMode.Create))
        //            await model.ImageFile.CopyToAsync(stream);
        //    }
        //    var entity = _mapper.Map<CategoryEntity>(model);
        //    entity.Image = imageName;
        //    _context.Categories.Add(entity);
        //    _context.SaveChanges();
        //    return Ok(entity);
        //}
    }
}
