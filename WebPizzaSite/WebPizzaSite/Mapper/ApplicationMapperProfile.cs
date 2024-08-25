using AutoMapper;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Models.Category;
using WebPizzaSite.Models.Product;

namespace WebPizzaSite.Mapper;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<CategoryEntity, CategoryItemViewModel>()
            .ForMember(opt=>opt.Image, val=>val.MapFrom(x=>"/uploads/"+x.Image));
        CreateMap<CategoryCreateViewModel, CategoryEntity>()
            .ForMember(opt=>opt.Image, val=>val.Ignore());

        CreateMap<ProductEntity, ProductItemViewModel>()
            .ForMember(opt => opt.Images, 
                val => val.MapFrom(x => x.ProductImages != null ? x.ProductImages.Select(img => img.Name).ToList() : new List<string>()));
    }
}
