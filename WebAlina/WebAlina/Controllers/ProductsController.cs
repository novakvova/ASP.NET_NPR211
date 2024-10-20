using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using WebAlina.Data;
using WebAlina.Data.Entities;
using WebAlina.Interfaces;
using WebAlina.Models.Product;

namespace WebAlina.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly AlinaDbContext _context;
        public readonly IConfiguration _configuration;
        public readonly IImageHulk _imageHulk;
        public readonly IMapper _mapper;
        public ProductsController(AlinaDbContext context, IConfiguration configuration, 
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
            var list = await _context.Products
                .ProjectTo<ProductItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateViewModel model)
        {
            var entity = _mapper.Map<ProductEntity>(model);
            _context.Products.Add(entity);
            _context.SaveChanges();

            if (model.Images != null)
            {
                var p = 1;
                foreach (var image in model.Images)
                {
                    var imageName = await _imageHulk.Save(image);
                    var imageProduct = new ProductImageEntity
                    {
                        Product = entity,
                        Image = imageName,
                        Priority = p++
                    };
                    _context.Add(imageProduct);
                    _context.SaveChanges();
                }
            }
            return Ok(entity.Id);
        }
    }
}
