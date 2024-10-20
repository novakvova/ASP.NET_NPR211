using AutoMapper;
using WebAlina.Data.Entities;
using WebAlina.Models.Category;
using WebAlina.Models.Product;

namespace WebAlina.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>();

            CreateMap<CategoryCreateViewModel, CategoryEntity>()
                .ForMember(x=>x.Image, opt=>opt.Ignore());

            CreateMap<ProductEntity, ProductItemViewModel>()
                .ForMember(x => x.CategoryName, opt => 
                    opt.MapFrom(x => x.Category == null ? String.Empty : x.Category.Name))
                .ForMember(x => x.Images, opt =>
                    opt.MapFrom(x => x.ProductImages == null ? 
                        new List<string>() : x.ProductImages.Select(pi=>pi.Image).ToList()));

            CreateMap<ProductCreateViewModel, ProductEntity>()
                .ForMember(x => x.ProductImages, opt => opt.Ignore());
        }
    }
}
