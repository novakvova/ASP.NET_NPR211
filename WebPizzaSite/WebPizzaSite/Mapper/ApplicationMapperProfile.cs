using AutoMapper;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Models.Category;

namespace WebPizzaSite.Mapper;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<CategoryEntity, CategoryItemViewModel>()
            .ForMember(opt=>opt.Image, val=>val.MapFrom(x=>"/uploads/"+x.Image));
        CreateMap<CategoryCreateViewModel, CategoryEntity>()
            .ForMember(opt=>opt.Image, val=>val.Ignore());
    }
}
