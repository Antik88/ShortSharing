using AutoMapper;
using ShortSharing.API.Dtos.CategoryDtos;
using ShortSharing.API.Dtos.RentDtos;
using ShortSharing.API.Dtos.ThingDtos;
using ShortSharing.API.Dtos.TypeDtos;
using ShortSharing.API.Dtos.UserDtos;
using ShortSharing.BLL.Models;

namespace ShortSharing.API.Mappers;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ThingModel, CreateThingDto>().ReverseMap();
        CreateMap<ThingDto, ThingModel>().ReverseMap();
        CreateMap<CategoryModel, CategoryDto>().ReverseMap();
        CreateMap<TypeModel, TypeDto>().ReverseMap();
        CreateMap<RentModel, RentDto>().ReverseMap();
        CreateMap<UserModel, UserDto>().ReverseMap();
    }
}
