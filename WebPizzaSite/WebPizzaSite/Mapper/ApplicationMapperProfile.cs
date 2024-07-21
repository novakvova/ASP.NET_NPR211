using AutoMapper;
using WebPizzaSite.Data.Entities;
using WebPizzaSite.Models.Category;

namespace WebPizzaSite.Mapper;

public class ApplicationMapperProfile : Profile
{
    public ApplicationMapperProfile()
    {
        CreateMap<CategoryEntity, CategoryItemViewModel>();
        CreateMap<CategoryCreateViewModel, CategoryEntity>();
    }
}
