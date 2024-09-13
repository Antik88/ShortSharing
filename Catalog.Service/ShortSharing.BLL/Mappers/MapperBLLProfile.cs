using AutoMapper;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Mappers
{
    public sealed class MapperBllProfile : Profile
    {
        public MapperBllProfile()
        {
            CreateMap<TypeEntity, TypeModel>().ReverseMap();

            CreateMap<CategoryEntity, CategoryModel>().ReverseMap();

            CreateMap<ThingEntity, ThingModel>().ReverseMap();
           
            CreateMap<ImageEntity, ImageModel>().ReverseMap();
        }
    }
}
