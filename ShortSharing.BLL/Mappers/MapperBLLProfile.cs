using AutoMapper;
using ShortSharing.BLL.Models;
using ShortSharing.DAL.Entities;

namespace ShortSharing.BLL.Mappers
{
    public sealed class MapperBllProfile : Profile
    {
        public MapperBllProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();

            CreateMap<TypeEntity, TypeModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<CategoryEntity, CategoryModel>()
                .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types))
                .ReverseMap();

            CreateMap<RentEntity, RentModel>().ReverseMap();

            CreateMap<ThingEntity, ThingModel>().ReverseMap();
        }
    }
}
